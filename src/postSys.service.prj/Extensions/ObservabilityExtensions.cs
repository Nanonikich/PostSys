using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace PostSys.Service.Extensions;

/// <summary>Методы расширения для метрик и трассировки запросов.</summary>
public static class ObservabilityExtensions
{
    /// <summary>Имя службы в системе наблюдения.</summary>
    public const string ServiceName = "PostSys.Service";

    /// <summary>Путь выдачи метрик в формате Prometheus.</summary>
    public const string MetricsPath = "/metrics";

    /// <summary>Регистрирует OpenTelemetry (метрики и трассировка).</summary>
    /// <param name="services">Коллекция служб.</param>
    /// <returns>Та же коллекция служб.</returns>
    public static IServiceCollection AddPostSysObservability(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services
            .AddOpenTelemetry()
            .ConfigureResource(resource => resource
                .AddService(
                    serviceName: ServiceName,
                    serviceVersion: typeof(ObservabilityExtensions).Assembly.GetName().Version?.ToString() ?? "0.0.0",
                    serviceInstanceId: Environment.MachineName))
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation(options =>
                    {
                        options.Filter = context =>
                            !IsProbeOrMetricsPath(context.Request.Path.Value);
                    })
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddNpgsql();
            })
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddPrometheusExporter();
            });

        return services;
    }

    /// <summary>Сопоставляет конечную точку метрик Prometheus.</summary>
    /// <param name="app">Конвейер приложения.</param>
    /// <returns>Тот же конвейер.</returns>
    public static WebApplication MapPostSysMetrics(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);
        app.MapPrometheusScrapingEndpoint(MetricsPath);
        return app;
    }

    private static bool IsProbeOrMetricsPath(string? path)
    {
        if(string.IsNullOrEmpty(path))
            return false;

		return path.StartsWith("/health", StringComparison.OrdinalIgnoreCase)
			|| path.Equals("/liveness", StringComparison.OrdinalIgnoreCase)
			|| path.Equals("/readiness", StringComparison.OrdinalIgnoreCase)
			|| path.Equals(MetricsPath, StringComparison.OrdinalIgnoreCase);
    }
}