using System;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate.Execution;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using PostSys.Domain.Packages.Checkers;
using PostSys.Domain.Postmen.Checkers;
using PostSys.IntegrationTests.Common;
using PostSys.IntegrationTests.Common.Docker.Database;

namespace PostSys.IntegrationTests.Api;

/// <summary>Представляет базовую фикстуру для проверки обработки запросов/команд.</summary>
public abstract class ApiHandlerTestFixtureBase : DatabaseTestBase
{
	#region Data

	private TestApplicationFactory<Program>? _factory;
	protected IRequestExecutor RequestExecutor;

	#endregion

	#region Properties

	/// <summary>Возвращает или устанавливает провайдер зарегистрированных сервисов.</summary>
	/// <value>Провайдер зарегистрированных сервисов.</value>
	protected IServiceProvider ServiceProvider { get; private set; }

	/// <summary>Возвращает или устанавливает замокированный чекер свойств почтальона.</summary>
	/// <value>Замокированный чекер свойств почтальона.</value>
	protected Mock<IPostmanParametersChecker> PostmanCheckerMock { get; private set; }

	/// <summary>Возвращает или устанавливает замокированный чекер свойств посылки.</summary>
	/// <value>Замокированный чекер свойств посылки.</value>
	protected Mock<IPackageParametersChecker> PackageCheckerMock { get; private set; }

	#endregion

	#region Methods

	/// <summary>Действия по окончанию тестов.</summary>
	[OneTimeTearDown]
	public void OneTimeTearDown()
	{
		_factory?.Dispose();
	}

	/// <summary>Настройка перед каждым тестом.</summary>
	[SetUp]
	public void SetUp()
	{
		SetSettingsOfPostmanChanger();
		SetSettingsOfPackageChanger();
	}

	private void SetSettingsOfPostmanChanger()
	{
		PostmanCheckerMock
			.Setup(service => service.CheckUniqueEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(true);
		PostmanCheckerMock
			.Setup(service => service.CheckExistenceOfPackageAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(true);
		PostmanCheckerMock
			.Setup(
				service => service.CheckPackageIsNotInOperationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(true);
	}

	private void SetSettingsOfPackageChanger()
	{
		PackageCheckerMock
			.Setup(service => service.CheckExistenceOfClientAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(true);
		PackageCheckerMock
			.Setup(service => service.CheckExistenceOfPostmanAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(true);
	}

	/// <inheritdoc/>
	[OneTimeSetUp]
	protected override async Task OneTimeSetUpAsync()
	{
		await base.OneTimeSetUpAsync();
		_factory = new TestApplicationFactory<Program>(
			SystemUnderTest.DataSourceConnectionString,
			services =>
			{
				PostmanCheckerMock = new Mock<IPostmanParametersChecker>();
				PackageCheckerMock = new Mock<IPackageParametersChecker>();
				services
					.AddSingleton(PostmanCheckerMock.Object)
					.AddSingleton(PackageCheckerMock.Object);
			});
		ServiceProvider = _factory.Services;
		RequestExecutor = await ServiceProvider
			.GetRequiredService<IRequestExecutorResolver>()
			.GetRequestExecutorAsync();
	}

	#endregion
}