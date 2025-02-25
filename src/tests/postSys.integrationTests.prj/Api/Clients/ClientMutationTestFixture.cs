using System;
using System.Threading.Tasks;

using Snapshooter.NUnit;

using PostSys.IntegrationTests.Api.Extensions;
using PostSys.IntegrationTests.Common.Extensions;
using PostSys.ReadModels.Helpers;

namespace PostSys.IntegrationTests.Api.Clients;

/// <summary>Представляет набор тестов мутаций.</summary>
[TestFixture]
public class ClientMutationTestFixture : ApiHandlerTestFixtureBase
{
	[Test]
	[Description("Проверяет создание клиента.")]
	public async Task Create_client_success()
	{
		//Act
		var result = await ClientTestHelper.CreateClientAsync(RequestExecutor);

		//Assert
		var id = result.GetIdFromResult<Guid>("createClient");
		result = await ClientTestHelper.GetClientByIdAsync(RequestExecutor, id);
		result.AssertErrors();
		result.MatchSnapshot();
	}

	[Test]
	[Description("Проверяет изменение данных об имени клиента.")]
	public async Task Change_client_fullname_success()
	{
		//Arrange
		var result = await ClientTestHelper.CreateClientAsync(RequestExecutor);
		var id = result.GetIdFromResult<Guid>("createClient");

		//Act
		result = await ClientTestHelper.ChangeClientFullnameAsync(RequestExecutor, id, new Fullname
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
	[Description("Проверяет изменение номера телефона клиента.")]
	public async Task Change_client_phone_number_success()
	{
		//Arrange
		var result = await ClientTestHelper.CreateClientAsync(RequestExecutor);
		var id = result.GetIdFromResult<Guid>("createClient");

		//Act
		result = await ClientTestHelper.ChangeClientPhoneNumberAsync(RequestExecutor, id, "8968-355-50-55");

		//Assert
		result.AssertErrors();
		result.MatchSnapshot();
	}

	[Test]
	[Description("Проверяет изменение номера телефона клиента на некорректный.")]
	public async Task Change_client_phone_number_to_incorrect_impossible()
	{
		//Arrange
		var result = await ClientTestHelper.CreateClientAsync(RequestExecutor);
		var id = result.GetIdFromResult<Guid>("createClient");

		//Act
		result = await ClientTestHelper.ChangeClientPhoneNumberAsync(RequestExecutor, id, "111");

		//Assert
		result.MatchSnapshot();
	}

	[Test]
	[Description("Проверяет изменение номера телефона клиента на пустоту.")]
	public async Task Change_client_phone_number_to_empty_impossible()
	{
		//Arrange
		var result = await ClientTestHelper.CreateClientAsync(RequestExecutor);
		var id = result.GetIdFromResult<Guid>("createClient");

		//Act
		result = await ClientTestHelper.ChangeClientPhoneNumberAsync(RequestExecutor, id, "");

		//Assert
		result.MatchSnapshot();
	}

	[Test]
	[Description("Проверяет удаление клиента.")]
	public async Task Delete_client_success()
	{
		//Arrange
		var firstClient = await ClientTestHelper.CreateClientAsync(RequestExecutor);
		var firstId = firstClient.GetIdFromResult<Guid>("createClient");
		await ClientTestHelper.CreateClientAsync(RequestExecutor, new Fullname
		{
			Surname = "default_surname1",
			Name = "default_name1",
			Patronymic = "default_patronymic1"
		});

		//Act
		await ClientTestHelper.DeleteClientAsync(RequestExecutor, firstId);

		//Assert
		var result = await RequestExecutor.GetClientsAsync();
		result.AssertErrors();
		result.MatchSnapshot(options => options.IgnoreAllFields("id"));
	}
}