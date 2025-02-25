using System;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using NUnit.Framework;

using PostSys.Domain.ValueObjects;

namespace PostSys.UnitTests.Packages;

/// <summary>Определяет тесты для проверки действий над сущностью "Посылка".</summary>
[TestFixture]
public class PackageMutationsTestFixture
{
	[Test]
	[Description("Проверяет изменение данных о посылке.")]
	public async Task Change_package_success()
	{
		// Arrange
		var package = await PackageTestHelper.CreatePackageAsync();
		var mockPackageParametersChecker = PackageTestHelper
			.GetPackageParametersCheckerMock().Object;

		// Act & Assert
		Assert.DoesNotThrowAsync(
			() => package.ChangeAsync(Guid.NewGuid(), Guid.NewGuid(), mockPackageParametersChecker));
	}

	[Test]
	[Description("Проверяет изменение параметров посылки.")]
	public async Task Change_package_dimensions_success()
	{
		// Arrange
		var package = await PackageTestHelper.CreatePackageAsync();

		// Act & Assert
		Assert.DoesNotThrow(() => package.ChangeDimensions(Dimensions.Create(22, 16, 89).Value));
	}

	[Test]
	[Description("Проверяет изменение параметров посылки на некорректные.")]
	public async Task Change_package_dimensions_to_incorrect_impossible()
	{
		// Arrange
		var package = await PackageTestHelper.CreatePackageAsync();

		// Act & Assert
		Assert.Throws<ResultFailureException>(() => package.ChangeDimensions(Dimensions.Create(-5, 16, 89).Value));
	}

	[Test]
	[Description("Проверяет изменение адреса посылки.")]
	public async Task Change_package_address_success()
	{
		// Arrange
		var package = await PackageTestHelper.CreatePackageAsync();

		// Act & Assert
		Assert.DoesNotThrow(() => package.ChangeAddress(Address.Create(177, 80).Value));
	}

	[Test]
	[Description("Проверяет изменение адреса посылки на пустой адрес.")]
	public async Task Change_package_address_to_incorrect_impossible()
	{
		// Arrange
		var package = await PackageTestHelper.CreatePackageAsync();

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() => package.ChangeAddress(null));
	}

	[Test]
	[Description("Проверяет изменение статуса посылки.")]
	public async Task Change_package_status_success()
	{
		// Arrange
		var package = await PackageTestHelper.CreatePackageAsync();

		// Act & Assert
		Assert.DoesNotThrow(() => package.ChangeStatus(PackageStatus.Create(Domain.Contracts.PackageStatus.End).Value));
	}
}