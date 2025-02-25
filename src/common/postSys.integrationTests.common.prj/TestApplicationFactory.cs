using System;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace PostSys.IntegrationTests.Common;

/// <summary>Представляет фабрику для создания тестируемого приложения.</summary>
/// <typeparam name="TEntryPoint">Точка входа приложения.</typeparam>
public class TestApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
{
	#region Data

	private readonly Action<IServiceCollection>? _actionTestDependencies;
	private readonly string? _postgresConnectionString;

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="TestApplicationFactory{TEntryPoint}" /></summary>
	/// <param name="postgresConnectionString">Строка подключения для контейнера.</param>
	/// <param name="actionTestDependencies">Действие для настройки зависимостей теста.</param>
	public TestApplicationFactory(
		string? postgresConnectionString,
		Action<IServiceCollection>? actionTestDependencies = null)
	{
		_postgresConnectionString = postgresConnectionString;
		_actionTestDependencies = actionTestDependencies;
	}

	#endregion

	#region Methods

	/// <inheritdoc />
	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.UseEnvironment("Testing");
		if(_postgresConnectionString != null)
			builder.UseSetting("ConnectionStrings:PostgresConnection", _postgresConnectionString);

		builder.ConfigureTestServices(services =>
		{
			var testDependencies = _actionTestDependencies;
			if(testDependencies == null) return;

			testDependencies(services);
		});
	}

	#endregion
}