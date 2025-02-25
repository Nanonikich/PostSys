using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using PostSys.Dal.Common.UnitOfWork;

namespace PostSys.Dal.Common.Extensions;

/// <summary>
/// Определяет расширения <see cref="IServiceCollection"/> для интеграции с EntityFramework Core.
/// </summary>
public static class EntityFrameworkDependencyInjectionExtensions
{
	#region Methods

	/// <summary>Добавляет зависимости UnitOfWork для работы с данными.</summary>
	/// <typeparam name="TContext">Тип контекста базы данных.</typeparam>
	/// <param name="serviceCollection">Экземпляр <see cref="IServiceCollection"/>.</param>
	/// <param name="uowLifetime">Время жизни UnitOfWork.</param>
	/// <param name="uowFactoryLifetime">Время жизни UnitOfWorkFactory.</param>
	/// <returns>Настроенный экземпляр <see cref="IServiceCollection"/>.</returns>
	public static IServiceCollection AddUnitOfWork<TContext>(
		this IServiceCollection serviceCollection,
		ServiceLifetime uowLifetime = ServiceLifetime.Scoped,
		ServiceLifetime uowFactoryLifetime = ServiceLifetime.Scoped) where TContext : DbContext
	{
		serviceCollection.TryAdd(
			new ServiceDescriptor(
				typeof(UnitOfWork<TContext>),
				typeof(UnitOfWork<TContext>),
				uowLifetime));

		serviceCollection.TryAdd(
			new ServiceDescriptor(
				typeof(IUnitOfWork),
				typeof(UnitOfWork<TContext>),
				uowLifetime));

		serviceCollection.TryAdd(
			new ServiceDescriptor(
				typeof(UnitOfWorkFactory<TContext>),
				typeof(UnitOfWorkFactory<TContext>),
				uowFactoryLifetime));

		serviceCollection.TryAdd(
			new ServiceDescriptor(
				typeof(IUnitOfWorkFactory),
				typeof(UnitOfWorkFactory<TContext>),
				uowFactoryLifetime));

		return serviceCollection;
	}

	#endregion
}