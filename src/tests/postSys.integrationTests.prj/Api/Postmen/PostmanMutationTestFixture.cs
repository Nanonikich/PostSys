using System;
using System.Threading;
using System.Threading.Tasks;

using Moq;

using Snapshooter.NUnit;

using PostSys.IntegrationTests.Api.Extensions;
using PostSys.IntegrationTests.Common.Extensions;
using PostSys.ReadModels.Helpers;

namespace PostSys.IntegrationTests.Api.Postmen;

/// <summary>Представляет набор тестов мутаций.</summary>
[TestFixture]
public class PostmanMutationTestFixture : ApiHandlerTestFixtureBase
{
	[Test]
	[Description("Проверяет создание почтальона.")]
	public async Task Create_postman_success()
	{
		//Act
		var result = await PostmanTestHelper.CreatePostmanAsync(RequestExecutor);

		//Assert
		var id = result.GetIdFromResult<Guid>("createPostman");
		result = await PostmanTestHelper.GetPostmanByIdAsync(RequestExecutor, id);
		result.AssertErrors();
		result.MatchSnapshot();
	}

	[Test]
	[Description("Проверяет изменение данных об имени почтальона.")]
	public async Task Change_postman_fullname_success()
	{
		//Arrange
		var result = await PostmanTestHelper.CreatePostmanAsync(RequestExecutor);
		var id = result.GetIdFromResult<Guid>("createPostman");

		//Act
		result = await PostmanTestHelper.ChangePostmanFullnameAsync(RequestExecutor, id, new Fullname
		{
			Surname = "default_surname1",
			Name = "default_name1",
			Patronymic = "default_patronymic1"
		});

		//Assert
		result.AssertErrors();
		result.MatchSnapshot();
	}

	[Test]
	[Description("Проверяет изменение Email почтальона.")]
	public async Task Change_postman_email_success()
	{
		//Arrange
		var result = await PostmanTestHelper.CreatePostmanAsync(RequestExecutor);
		var id = result.GetIdFromResult<Guid>("createPostman");

		//Act
		result = await PostmanTestHelper.ChangePostmanEmailAsync(RequestExecutor, id, "nanonano@gmail.com");

		//Assert
		result.AssertErrors();
		result.MatchSnapshot();
	}

	[Test]
	[Description("Проверяет уникальность изменяемого Email почтальона.")]
	public async Task Change_postman_email_to_nonunique_impossible()
	{
		//Arrange
		var result = await PostmanTestHelper.CreatePostmanAsync(RequestExecutor);
		var id = result.GetIdFromResult<Guid>("createPostman");
		PostmanCheckerMock
			.Setup(service => service.CheckUniqueEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(false);

		//Act
		result = await PostmanTestHelper.ChangePostmanEmailAsync(RequestExecutor, id, "nano@gmail.com");

		//Assert
		result.MatchSnapshot();
	}

	[Test]
	[Description("Проверяет изменение Email почтальона на некорректный.")]
	public async Task Change_postman_email_to_incorrect_impossible()
	{
		//Arrange
		var result = await PostmanTestHelper.CreatePostmanAsync(RequestExecutor);
		var id = result.GetIdFromResult<Guid>("createPostman");

		//Act
		result = await PostmanTestHelper.ChangePostmanEmailAsync(RequestExecutor, id, "111");

		//Assert
		result.MatchSnapshot();
	}

	[Test]
	[Description("Проверяет изменение пароля почтальона.")]
	public async Task Change_postman_password_success()
	{
		//Arrange
		var result = await PostmanTestHelper.CreatePostmanAsync(RequestExecutor);
		var id = result.GetIdFromResult<Guid>("createPostman");

		//Act
		result = await PostmanTestHelper.ChangePostmanPasswordAsync(RequestExecutor, id, "111111");

		//Assert
		result.AssertErrors();
		result.MatchSnapshot();
	}

	[Test]
	[Description("Проверяет изменение пароля почтальона на пароль малой длины.")]
	public async Task Change_postman_password_to_short_password_impossible()
	{
		//Arrange
		var result = await PostmanTestHelper.CreatePostmanAsync(RequestExecutor);
		var id = result.GetIdFromResult<Guid>("createPostman");

		//Act
		result = await PostmanTestHelper.ChangePostmanPasswordAsync(RequestExecutor, id, "1111");

		//Assert
		result.MatchSnapshot();
	}

	[Test]
	[Description("Проверяет удаление почтальона.")]
	public async Task Delete_postman_success()
	{
		//Arrange
		var firstPostman = await PostmanTestHelper.CreatePostmanAsync(RequestExecutor);
		var firstId = firstPostman.GetIdFromResult<Guid>("createPostman");
		await PostmanTestHelper.CreatePostmanAsync(RequestExecutor, new Fullname
		{
			Surname = "default_surname1",
			Name = "default_name1",
			Patronymic = "default_patronymic1"
		});

		//Act
		await PostmanTestHelper.DeletePostmanAsync(RequestExecutor, firstId);

		//Assert
		var result = await RequestExecutor.GetPostmenAsync();
		result.AssertErrors();
		result.MatchSnapshot(options => options.IgnoreAllFields("id"));
	}
}