using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Npgsql;

using NUnit.Framework;

using Testcontainers.PostgreSql;

namespace PostSys.IntegrationTests.Common.Docker.Database;

/// <summary>
/// Представляет настройки для запуска PostgreSQL в контейнере
/// с возможностью миграций схемы для всех test fixtures в тестовом scope.
/// </summary>
/// <remarks>
/// Определите файл appsettings.json с следующей структурой:
/// <code>
/// {
/// 	"DockerSettings": {
/// 		"UseContainer": true
/// 	}
/// }
/// </code>
///     В случае отсутствия необходимости в контейнере возможно явно указать строку подключения для доступа к экземпляру
///     pgs:
///     <code>
/// {
/// 	"ConnectionStrings": {
/// 		"TestConnection": "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres"
/// 	}
/// }
/// </code>
/// </remarks>
[SetUpFixture]
public abstract class PostgreSqlContainerSetup<TEntryPoint, TDbContext>
	where TEntryPoint : class where TDbContext : DbContext
{
	#region Data

	private PostgreSqlContainer? _container;
	private TestApplicationFactory<TEntryPoint>? _factory;
	private MigrationPerformer<TDbContext>? _migrator;

	#endregion

	#region Properties

	/// <summary>Возвращает флаг необходимости запуска миграций.</summary>
	protected virtual bool NeedMigrations { get; } = true;

	#endregion

	#region Methods

	/// <summary>Инициализирует окружение SUT.</summary>
	/// <value>Асинхронная операция.</value>
	[OneTimeSetUp]
	public async Task OneTimeSetUpAsync()
	{
		var (connectionString, containerConfiguration) = LoadConfiguration();

		if(containerConfiguration?.UseContainer == true)
		{
			_container = BuildContainer(containerConfiguration);
			await _container.StartAsync();
			connectionString = _container.GetConnectionString();
		}

		if(string.IsNullOrEmpty(connectionString))
			throw new InvalidOperationException(
				"Строка подключения к БД не задана в конфигурации (секция ConnectionStrings__TestConnection).");

		_factory = new TestApplicationFactory<TEntryPoint>(connectionString);

		if(NeedMigrations)
		{
			_migrator = new MigrationPerformer<TDbContext>(_factory.Services);
			try
			{
				await _migrator.MigrateUpAsync();
			}
			catch(PostgresException ex)
			{
				Console.WriteLine($"Ошибка миграции: {ex.Message}");
			}
		}

		SystemUnderTest.DataSourceConnectionString = connectionString;
	}

	/// <summary>Деинициализирует окружение SUT.</summary>
	/// <value>Асинхронная операция.</value>
	[OneTimeTearDown]
	public async Task OneTimeTearDownAsync()
	{
		if(_migrator != null) await _migrator.MigrateDownAsync();

		if(_factory != null) await _factory.DisposeAsync();

		await CleanupContainerAsync();
	}

	private async Task CleanupContainerAsync()
	{
		if(_container != null)
		{
			await _container.StopAsync();
			await _container.DisposeAsync();
		}
	}

	private static PostgreSqlContainer BuildContainer(ContainerConfiguration configuration) => new PostgreSqlBuilder()
		.WithImage(configuration.ImageName)
		.Build();

	private static (string?, ContainerConfiguration? containerConfiguration) LoadConfiguration()
	{
		var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false)
													  .AddEnvironmentVariables()
													  .Build();

		var containerConfiguration = configuration.GetRequiredSection("DockerSettings")
												  .Get<ContainerConfiguration>();
		var connectionString = configuration.GetConnectionString("TestConnection");
		return (connectionString, containerConfiguration);
	}

	#endregion

	#region Type: ContainerConfiguration

	private class ContainerConfiguration
	{
		/// <summary>Возвращает имя образа.</summary>
		/// <value>Имя образа.</value>
		[Required]
		public string ImageName { get; init; } = "postgres:latest";

		/// <summary>Возвращает значение, указывающее, что PostgreSQL для тестирования будет запущен в контейнере.</summary>
		/// <value>Значение, указывающее, что PostgreSQL для тестирования будет запущен в контейнере.</value>
		[Required]
		public bool UseContainer { get; init; } = true;
	}

	#endregion
}