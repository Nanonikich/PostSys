using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PostSys.Service.Common.Contracts;

namespace PostSys.Service.Extensions;

/// <summary>Сопоставление конечных точек проверки состояния для Docker и оркестраторов.</summary>
public static class HealthChecksEndpointExtensions
{
	/// <summary>Сопоставляет проверки жизнеспособности, готовности и общий обзор.</summary>
	/// <param name="app">Конвейер приложения.</param>
	/// <returns>Тот же конвейер.</returns>
	public static WebApplication MapPostSysHealthChecks(this WebApplication app)
	{
		ArgumentNullException.ThrowIfNull(app);

		// Процесс жив: без зависимостей (для livenessProbe).
		app.MapHealthChecks(HealthChecksConstants.LivenessUrlPattern, new HealthCheckOptions
		{
			Predicate = _ => false,
			ResponseWriter = WritePlainStatusAsync
		});

		// Готов принимать трафик: БД и прочие проверки с тегом Readiness.
		app.MapHealthChecks(HealthChecksConstants.ReadinessUrlPattern, new HealthCheckOptions
		{
			Predicate = check => check.Tags.Contains(HealthChecksConstants.ReadinessTag),
			ResponseWriter = WritePlainStatusAsync
		});

		// Совместимость с Docker / документацией.
		app.MapHealthChecks("/health", new HealthCheckOptions
		{
			Predicate = check => check.Tags.Contains(HealthChecksConstants.ReadinessTag),
			ResponseWriter = WritePlainStatusAsync
		});

		return app;
	}

	private static Task WritePlainStatusAsync(HttpContext context, HealthReport report)
	{
		context.Response.ContentType = "text/plain; charset=utf-8";
		return context.Response.WriteAsync(report.Status == HealthStatus.Healthy ? "Healthy" : report.Status.ToString());
	}
}
