using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using Moq;

using NUnit.Framework;

using PostSys.Domain.Events;
using PostSys.Domain.Postmen;
using PostSys.Domain.ValueObjects;

namespace PostSys.UnitTests.Postmen;

/// <summary>Определяет тесты для проверки действий над сущностью "Почтальон".</summary>
[TestFixture]
public class PostmanMutationsTestFixture
{
	[Test]
	[Description("Проверяет создание почтальона без имени.")]
	public async Task Create_postman_without_name_impossible()
	{
		// Arrange
		var email = Email.Create("ivanov@gmail.com");
		var password = PasswordHash.Create("hashed-password-value");
		var mockPostmanParametersChecker = PostmanTestHelper.GetPostmanParametersCheckerMock().Object;

		// Act & Assert
		await Assert.ThrowsAsync<ArgumentException>(() => Postman.CreateAsync(
			Fullname.Create("Филатов", string.Empty, string.Empty).Value,
			Guid.NewGuid(),
			email.Value,
			password.Value,
			mockPostmanParametersChecker));
	}

	[Test]
	[Description("Проверяет, что при создании почтальона поднимается доменное событие.")]
	public async Task Create_postman_raises_domain_event()
	{
		var postman = await PostmanTestHelper.CreatePostmanAsync();

		Assert.That(postman.DomainEvents.OfType<PostmanCreated>().Any(), Is.True);
		Assert.That(postman.Id, Is.Not.EqualTo(Guid.Empty));
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
		Assert.That(postman.DomainEvents.OfType<PostmanFullnameChanged>().Any(), Is.True);
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
		await Assert.DoesNotThrowAsync(() => postman.ChangeEmailAsync(newEmail.Value, mockPostmanParametersChecker));
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
		await Assert.ThrowsAsync<ArgumentException>(
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
		await Assert.ThrowsAsync<ResultFailureException>(
			() => postman.ChangeEmailAsync(newEmail.Value, mockPostmanParametersChecker));
	}

	[Test]
	[Description("Проверяет изменение пароля почтальона (принимает только хеш).")]
	public async Task Change_postman_password_success()
	{
		// Arrange
		var postman = await PostmanTestHelper.CreatePostmanAsync();
		var newPassword = PasswordHash.Create("new-hashed-password");

		// Act & Assert
		Assert.DoesNotThrow(() => postman.ChangePassword(newPassword.Value));
		Assert.That(postman.DomainEvents.OfType<PostmanPasswordChanged>().Any(), Is.True);
	}

	[Test]
	[Description("Проверяет, что пустой хеш пароля недопустим.")]
	public void Password_hash_requires_value()
	{
		Assert.That(PasswordHash.Create(string.Empty).IsFailure, Is.True);
		Assert.That(PasswordHash.Create("   ").IsFailure, Is.True);
	}
}
