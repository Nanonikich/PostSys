namespace PostSys.IntegrationTests.Common.Docker.Database;

/// <summary>Определяет настройки тестируемой системы.</summary>
public sealed class SystemUnderTest
{
	#region Properties

	/// <summary>Возвращает или задаёт строку подключения к базе данных.</summary>
	/// <value>Строка подключения к базе данных.</value>
	public static string DataSourceConnectionString { get; set; }

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="SystemUnderTest" />.</summary>
	/// <param name="dataSourceConnectionString">Строка подключения к БД.</param>
	private SystemUnderTest(string dataSourceConnectionString)
	{
		DataSourceConnectionString = dataSourceConnectionString;
	}

	#endregion

}