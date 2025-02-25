using System;
using System.Threading;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using Moq;

using NUnit.Framework;

using PostSys.Domain.Postmen;
using PostSys.Domain.ValueObjects;

namespace PostSys.UnitTests.Postmen;

/// <summary>Определяет тесты для проверки действий над сущностью "Почтальон".</summary>
[TestFixture]
public class PostmanMutationsTestFixture
{
	[Test]
	[Description("Проверяет создание почтальона без имени.")]
	public void Create_postman_without_name_impossible()
	{
		// Arrange
		var email = Email.Create("ivanov@gmail.com");
		var password = Password.Create("12321Hope");
		var mockPostmanParametersChecker = PostmanTestHelper.GetPostmanParametersCheckerMock().Object;

		// Act & Assert
		Assert.ThrowsAsync<ArgumentException>(() => Postman.CreateAsync(
			Fullname.Create("Филатов", string.Empty, string.Empty).Value,
			Guid.NewGuid(),
			email.Value,
			password.Value,
			mockPostmanParametersChecker));
	}

	[Test]
	[Description("Проверяет изменение данных об имени почтальона.")]
	public async Task Change_postman_fullname_success()
	{
		// Arrange
		var postman = await PostmanTestHelper.CreatePostmanAsync();
		var newFullname = Fullname.Create("Иванов", "Герман", "Иванович");

		// Act & Assert
		Assert.DoesNotThrow(() => postman.ChangeFullname(newFullname.Value));
	}

	[Test]
	[Description("Проверяет изменение Email почтальона.")]
	public async Task Change_postman_email_success()
	{
		// Arrange
		var postman = await PostmanTestHelper.CreatePostmanAsync();
		var mockPostmanParametersChecker = PostmanTestHelper.GetPostmanParametersCheckerMock().Object;
		var newEmail = Email.Create("newIvanov@gmail.com");

		// Act & Assert
		Assert.DoesNotThrowAsync(() => postman.ChangeEmailAsync(newEmail.Value, mockPostmanParametersChecker));
	}

	[Test]
	[Description("Проверяет уникальность изменяемого Email почтальона.")]
	public async Task Change_postman_email_to_nonunique_impossible()
	{
		// Arrange
		var postman = await PostmanTestHelper.CreatePostmanAsync();
		var mockPostmanParametersChecker = PostmanTestHelper.GetPostmanParametersCheckerMock();
		mockPostmanParametersChecker
			.Setup(service => service.CheckUniqueEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(false);
		var newEmail = Email.Create("ivanov@gmail.com");

		// Act & Assert
		Assert.ThrowsAsync<ArgumentException>(
			() => postman.ChangeEmailAsync(newEmail.Value, mockPostmanParametersChecker.Object));
	}

	[Test]
	[Description("Проверяет изменение Email почтальона на некорректный.")]
	public async Task Change_postman_email_to_icorrect_impossible()
	{
		// Arrange
		var postman = await PostmanTestHelper.CreatePostmanAsync();
		var mockPostmanParametersChecker = PostmanTestHelper.GetPostmanParametersCheckerMock().Object;
		var newEmail = Email.Create("111");

		// Act & Assert
		Assert.ThrowsAsync<ResultFailureException>(
			() => postman.ChangeEmailAsync(newEmail.Value, mockPostmanParametersChecker));
	}

	[Test]
	[Description("Проверяет изменение пароля почтальона.")]
	public async Task Change_postman_password_success()
	{
		// Arrange
		var postman = await PostmanTestHelper.CreatePostmanAsync();
		var newPassword = Password.Create("111TTTr");

		// Act & Assert
		Assert.DoesNotThrow(() => postman.ChangePassword(newPassword.Value));
	}

	[Test]
	[Description("Проверяет изменение пароля почтальона на пароль малой длины.")]
	public async Task Change_postman_password_to_short_password_impossible()
	{
		// Arrange
		var postman = await PostmanTestHelper.CreatePostmanAsync();

		// Act & Assert
		Assert.Throws<ResultFailureException>(() => postman.ChangePassword(Password.Create("1").Value));
	}
}