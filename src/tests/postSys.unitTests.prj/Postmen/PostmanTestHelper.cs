using System;
using System.Threading;
using System.Threading.Tasks;

using Moq;

using PostSys.Domain.Postmen;
using PostSys.Domain.Postmen.Checkers;
using PostSys.Domain.ValueObjects;

namespace PostSys.UnitTests.Postmen;

/// <summary>Вспомогательный класс для тестов сущности "Почтальон".</summary>
internal static class PostmanTestHelper
{
	#region Methods

	/// <summary>Мокирование чекера свойств почтальона.</summary>
	/// <param name="result">Результат выполнения методов.</param>
	/// <returns>Замокированный чекер свойств почтальона.</returns>
	public static Mock<IPostmanParametersChecker> GetPostmanParametersCheckerMock(bool result = true)
	{
		var res = new Mock<IPostmanParametersChecker>();
		res
			.Setup(service => service.CheckUniqueEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(result);
		res
			.Setup(service => service.CheckExistenceOfPackageAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(result);
		res
			.Setup(service => service.CheckPackageIsNotInOperationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(result);
		return res;
	}

	/// <summary>Создание почтальона для теста.</summary>
	/// <returns>Асинхронная операция, возвращающая почтальона.</returns>
	public static async Task<Postman> CreatePostmanAsync()
	{
		var fullname = Fullname.Create("Иванов", "Иван", "Иванович");
		var email = Email.Create("ivanov@gmail.com");
		var password = Password.Create("12321Hope");
		var mockPostmanParametersChecker = GetPostmanParametersCheckerMock().Object;
		return await Postman.CreateAsync(
			fullname.Value,
			Guid.NewGuid(),
			email.Value,
			password.Value,
			mockPostmanParametersChecker);
	}

	#endregion
}