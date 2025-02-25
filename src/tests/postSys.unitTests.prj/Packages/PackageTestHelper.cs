using System;
using System.Threading;
using System.Threading.Tasks;

using Moq;

using PostSys.Domain.Packages;
using PostSys.Domain.Packages.Checkers;
using PostSys.Domain.ValueObjects;

namespace PostSys.UnitTests.Packages;

/// <summary>Вспомогательный класс для тестов сущности "Посылка".</summary>
internal static class PackageTestHelper
{
	#region Methods

	/// <summary>Мокирование чекера свойств посылки.</summary>
	/// <param name="result">Результат выполнения методов.</param>
	/// <returns>Замокированный чекер свойств посылки.</returns>
	public static Mock<IPackageParametersChecker> GetPackageParametersCheckerMock(bool result = true)
	{
		var res = new Mock<IPackageParametersChecker>();
		res
			.Setup(service => service.CheckExistenceOfClientAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(result);
		res
			.Setup(service => service.CheckExistenceOfPostmanAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(result);
		return res;
	}

	/// <summary>Создание посылки для теста.</summary>
	/// <returns>Асинхронная операция, возвращающая посылку.</returns>
	public static async Task<Package> CreatePackageAsync()
	{
		var dimensions = Dimensions.Create(22, 24, 80);
		var address = Address.Create(168, 70);
		var status = PackageStatus.Create(Domain.Contracts.PackageStatus.OnWarehouse);
		var mockPackageParametersChecker = GetPackageParametersCheckerMock().Object;
		return await Package.CreateAsync(
			dimensions.Value,
			address.Value,
			status.Value,
			Guid.NewGuid(),
			Guid.NewGuid(),
			mockPackageParametersChecker);
	}

	#endregion
}