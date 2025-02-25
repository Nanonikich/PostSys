using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Snapshooter.NUnit;

using PostSys.IntegrationTests.Api.Extensions;
using PostSys.IntegrationTests.Common.Extensions;
using PostSys.ReadModels;
using PostSys.ReadModels.Helpers;

namespace PostSys.IntegrationTests.Api.Packages;

/// <summary>Представляет набор тестов для запросов.</summary>
[TestFixture]
public class PackageQueryTestFixture : ApiHandlerTestFixtureBase
{
	[Test]
	[Description("Проверяет получение посылок по идентификатору.")]
	public async Task Getting_packages_by_id()
	{
		//Arrange
		var result = await PackageTestHelper.CreatePackageAsync(RequestExecutor);
		var id = result.GetIdFromResult<Guid>("createPackage");

		//Act
		result = await PackageTestHelper.GetPackageByIdAsync(RequestExecutor, id);

		//Assert
		result.AssertErrors();
		result.MatchSnapshot(opt => opt
									.IgnoreAllFields("id")
									.IgnoreAllFields("packageClientId")
									.IgnoreAllFields("packagePostmanId"));
	}

	[Test]
	[Description("Проверяет получение списка посылок.")]
	public async Task Getting_packages_list()
	{
		//Arrange
		var inputs = new List<Package>
		{
			new()
			{
				Dimensions = new Dimensions { Length = 28, Height = 28, Weight = 32 },
				Address = new Address { Longitude = 32, Latitude = 22 }
			},
			new()
			{
				Dimensions = new Dimensions { Length = 30, Height = 22, Weight = 42 },
				Address = new Address { Longitude = 135, Latitude = 148 }
			}
		};

		foreach(var input in inputs)
			await PackageTestHelper.CreatePackageAsync(RequestExecutor, input.Dimensions, input.Address);

		//Act
		var result = await RequestExecutor.GetPackagesAsync();

		//Assert
		result.AssertErrors();
		result.MatchSnapshot(opt => opt
									.IgnoreAllFields("id")
									.IgnoreAllFields("packageClientId")
									.IgnoreAllFields("packagePostmanId"));
	}
}