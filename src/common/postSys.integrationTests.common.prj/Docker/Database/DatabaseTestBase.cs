using System.Linq;
using System.Threading.Tasks;

using Npgsql;

using NUnit.Framework;

using Respawn;
using Respawn.Graph;

namespace PostSys.IntegrationTests.Common.Docker.Database;

/// <summary>Описывает базовый класс для интеграционных тестов.</summary>
public abstract class DatabaseTestBase
{
	#region Data

	private NpgsqlConnection _connection;
	private NpgsqlDataSource _dataSource;
	private Respawner _respawner;

	#endregion

	#region Properties

	/// <summary>Возвращает или задаёт таблицы для исключения при очистке.</summary>
	/// <value>Таблицы для исключения при очистке.</value>
	public Table[] TablesToIgnore { get; protected set; } = [];

	#endregion

	#region Methods

	/// <summary>Деинициализирует подключение к базе данных.</summary>
	/// <returns>Асинхронная операция.</returns>
	[OneTimeTearDown]
	public async Task OneTimeTearDownAsync()
	{
		await _connection.CloseAsync();
		await _connection.DisposeAsync();
		await _dataSource.DisposeAsync();
	}

	/// <summary>Подготавливает базу данных к работе теста.</summary>
	/// <returns>Асинхронная операция.</returns>
	[SetUp]
	public virtual Task SetUpAsync() => _respawner.ResetAsync(_connection);

	/// <summary>Инициализирует окружение фикстуры.</summary>
	/// <returns>Асинхронная операция.</returns>
	[OneTimeSetUp]
	protected virtual async Task OneTimeSetUpAsync()
	{
		_dataSource = NpgsqlDataSource.Create(SystemUnderTest.DataSourceConnectionString);
		_connection = _dataSource.CreateConnection();
		await _connection.OpenAsync();
		_respawner = await Respawner.CreateAsync(
			_connection,
			new RespawnerOptions
			{
				TablesToIgnore =
				[
					"__EFMigrationsHistory",
					.. TablesToIgnore.Select((t => new Table(t.Name))),
				],
				SchemasToInclude = ["public"],
				DbAdapter = DbAdapter.Postgres
			});
	}

	#endregion
}