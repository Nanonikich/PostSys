using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using PostSys.Dal.Common.Extensions;
using PostSys.Dal.Persistence;
using PostSys.Dal.Persistence.Clients;
using PostSys.Dal.Persistence.Packages;
using PostSys.Dal.Persistence.Postmen;
using PostSys.Dal.ReadModel;
using PostSys.Dal.Services;

using PostSys.Domain.Clients;
using PostSys.Domain.Packages;
using PostSys.Domain.Packages.Checkers;
using PostSys.Domain.Postmen;
using PostSys.Domain.Postmen.Checkers;

namespace PostSys.Dal.Extensions;

/// <summary>Определяет методы расширения для настройки инструментов хранения данных.</summary>
public static class DataStoragesExtensions
{
	#region Methods

	/// <summary>Регистрирует data storages для использования в сервисе.</summary>
	/// <param name="serviceCollection"><see cref="IServiceCollection" />.</param>
	/// <param name="databaseConnectionString">Строка подключения к БД.</param>
	public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, string databaseConnectionString)
	{
		ArgumentNullException.ThrowIfNull(serviceCollection);
		ArgumentException.ThrowIfNullOrWhiteSpace(databaseConnectionString);
		serviceCollection
			.AddDbContextPool<WriteDbContext>(SetupOptions)
			.AddPooledDbContextFactory<ReadDbContext>(SetupOptions)
			.AddDbContextPool<ReadDbContext>(SetupOptions)
			.AddUnitOfWork<WriteDbContext>()
			.AddRepositories()
			.AddServices();

		return serviceCollection;

		void SetupOptions(DbContextOptionsBuilder builder)
		{
			builder
				.UseNpgsql(databaseConnectionString)
				.UseSnakeCaseNamingConvention();
		}
	}

	private static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
	{
		return serviceCollection
			.AddScoped<IPostmanRepository, PostmanRepository>()
			.AddScoped<IClientRepository, ClientRepository>()
			.AddScoped<IPackageRepository, PackageRepository>();
	}

	private static IServiceCollection AddServices(this IServiceCollection serviceCollection)
	{
		return serviceCollection
			.AddScoped<IPostmanParametersChecker, PostmanParametersChecker>()
			.AddScoped<IPackageParametersChecker, PackageParametersChecker>();
	}

	#endregion
}