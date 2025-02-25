using System;
using System.Threading;
using System.Threading.Tasks;

using Moq;

using Snapshooter.NUnit;

using PostSys.IntegrationTests.Api.Clients;
using PostSys.IntegrationTests.Api.Extensions;
using PostSys.IntegrationTests.Api.Postmen;
using PostSys.IntegrationTests.Common.Extensions;
using PostSys.ReadModels.Helpers;

namespace PostSys.IntegrationTests.Api.Packages;

/// <summary>Представляет набор тестов мутаций.</summary>
[TestFixture]
public class PackageMutationTestFixture : ApiHandlerTestFixtureBase
{
	[Test]
	[Description("Проверяет создание посылки.")]
	public async Task Create_package_success()
	{
		//Act
		var result = await PackageTestHelper.CreatePackageAsync(RequestExecutor);

		//Assert
		var id = result.GetIdFromResult<Guid>("createPackage");
		result = await PackageTestHelper.GetPackageByIdAsync(RequestExecutor, id);
		result.AssertErrors();
		result.MatchSnapshot(opt => opt
									.IgnoreAllFields("id")
									.IgnoreAllFields("packageClientId")
									.IgnoreAllFields("packagePostmanId"));
	}

	[Test]
	[Description("Проверяет изменение данных о посылке.")]
	public async Task Change_package_success()
	{
		//Arrange
		var result = await PackageTestHelper.CreatePackageAsync(RequestExecutor);
		var id = result.GetIdFromResult<Guid>("createPackage");
		var clientId = (await ClientTestHelper.CreateClientAsync(RequestExecutor))
			.GetIdFromResult<Guid>("createClient");
		var postmanId = (await PostmanTestHelper.CreatePostmanAsync(RequestExecutor))
			.GetIdFromResult<Guid>("createPostman");

		//Act
		result = await PackageTestHelper.ChangePackageAsync(
			RequestExecutor,
			id,
			clientId,
			postmanId);

		//Assert
		result.AssertErrors();
		result.MatchSnapshot();
	}

	[Test]
	[Description("Проверяет изменение данных посылки на несуществующие.")]
	public async Task Change_package_if_data_is_null_impossible()
	{
		//Arrange
		var result = await PackageTestHelper.CreatePackageAsync(RequestExecutor);
		var id = result.GetIdFromResult<Guid>("createPackage");
		PackageCheckerMock
			.Setup(service => service.CheckExistenceOfClientAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(false);

		//Act
		result = await PackageTestHelper.ChangePackageAsync(
			RequestExecutor,
			id,
			Guid.NewGuid(),
			Guid.NewGuid());

		//Assert
		result.MatchSnapshot();
	}

	[Test]
	[Description("Проверяет изменение параметров посылки.")]
	public async Task Change_package_dimensions_success()
	{
		//Arrange
		var result = await PackageTestHelper.CreatePackageAsync(RequestExecutor);
		var id = result.GetIdFromResult<Guid>("createPackage");

		//Act
		result = await PackageTestHelper.ChangePackageDimensionsAsync(RequestExecutor, id, new Dimensions
		{
			Length = 30,
			Height = 22,
			Weight = 42
		});

		//Assert
		result.AssertErrors();
		result.MatchSnapshot();
	}

	[Test]
	[Description("Проверяет изменение параметров посылки на некорректные.")]
	public async Task Change_package_dimensions_to_incorrect_impossible()
	{
		//Arrange
		var result = await PackageTestHelper.CreatePackageAsync(RequestExecutor);
		var id = result.GetIdFromResult<Guid>("createPackage");

		//Act
		result = await PackageTestHelper.ChangePackageDimensionsAsync(RequestExecutor, id, new Dimensions
		{
			Length = -5,
			Height = 16,
			Weight = 89
		});

		//Assert
		result.MatchSnapshot();
	}

	[Test]
	[Description("Проверяет изменение адреса посылки.")]
	public async Task Change_package_address_success()
	{
		//Arrange
		var result = await PackageTestHelper.CreatePackageAsync(RequestExecutor);
		var id = result.GetIdFromResult<Guid>("createPackage");

		//Act
		result = await PackageTestHelper.ChangePackageAddressAsync(RequestExecutor, id, new Address
		{
			Longitude = 135,
			Latitude = 148
		});

		//Assert
		result.AssertErrors();
		result.MatchSnapshot();
	}

	[Test]
	[Description("Проверяет изменение статуса посылки.")]
	public async Task Change_package_status_success()
	{
		//Arrange
		var result = await PackageTestHelper.CreatePackageAsync(RequestExecutor);
		var id = result.GetIdFromResult<Guid>("createPackage");

		//Act
		result = await PackageTestHelper.ChangePackageStatusAsync(
			RequestExecutor, 
			id, 
			ReadModels.Contracts.PackageStatus.OnWarehouse);

		//Assert
		result.AssertErrors();
		result.MatchSnapshot();
	}

	[Test]
	[Description("Проверяет удаление посылки.")]
	public async Task Delete_package_success()
	{
		//Arrange
		var firstPackage = await PackageTestHelper.CreatePackageAsync(RequestExecutor);
		var firstId = firstPackage.GetIdFromResult<Guid>("createPackage");
		await PackageTestHelper.CreatePackageAsync(
			RequestExecutor,
			new Dimensions
			{
				Length = 28, Height = 28, Weight = 32
			},
			new Address
			{
				Longitude = 32, Latitude = 22
			});

		//Act
		await PackageTestHelper.DeletePackageAsync(RequestExecutor, firstId);

		//Assert
		var result = await RequestExecutor.GetPackagesAsync();
		result.AssertErrors();
		result.MatchSnapshot(opt => opt
									.IgnoreAllFields("id")
									.IgnoreAllFields("packageClientId")
									.IgnoreAllFields("packagePostmanId"));
	}
}