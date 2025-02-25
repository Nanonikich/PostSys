using System.Reflection;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using PostSys.Dal.Common.UnitOfWork;
using PostSys.Service.Common.Behaviors;

namespace PostSys.Service.Extensions;

/// <summary>Определяет расширения для поддержки CQRS-pattern.</summary>
public static class CqrsExtensions
{
	#region Methods

	/// <summary>Регистрирует зависимости для добавления CQRS.</summary>
	/// <param name="services"><see cref="IServiceCollection"/>.</param>
	/// <param name="serviceLifetime"><see cref="ServiceLifetime"/> управления обработчиков команд/запросов.</param>
	/// <param name="assemblies">Сборки для сканирования.</param>
	/// <returns><see cref="IServiceCollection"/>.</returns>
	public static IServiceCollection AddCqrs(
		this IServiceCollection services,
		ServiceLifetime serviceLifetime = ServiceLifetime.Scoped,
		params Assembly[] assemblies)
	{
		Assembly[] assemblyArray;
		if(assemblies.Length == 0)
			assemblyArray =
			[
				Assembly.GetCallingAssembly(),
				Assembly.GetExecutingAssembly()
			];
		else
			assemblyArray = [.. assemblies, Assembly.GetExecutingAssembly()];

		var assembliesToScan = assemblyArray;
		return services
			.AddMediatR(configuration =>
			{
				configuration.RegisterServicesFromAssemblies(assembliesToScan);
				configuration.Lifetime = serviceLifetime;
			})
			.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
			.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
	}

	#endregion
}