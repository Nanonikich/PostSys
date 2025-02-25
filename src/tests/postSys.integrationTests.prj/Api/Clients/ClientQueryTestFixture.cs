using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Snapshooter.NUnit;

using PostSys.IntegrationTests.Api.Extensions;
using PostSys.IntegrationTests.Common.Extensions;
using PostSys.ReadModels.Helpers;

namespace PostSys.IntegrationTests.Api.Clients;

/// <summary>Представляет набор тестов для запросов.</summary>
[TestFixture]
public class ClientQueryTestFixture : ApiHandlerTestFixtureBase
{
	[Test]
	[Description("Проверяет получение клиентов по идентификатору.")]
	public async Task Getting_clients_by_id()
	{
		//Arrange
		var result = await ClientTestHelper.CreateClientAsync(RequestExecutor);
		var id = result.GetIdFromResult<Guid>("createClient");

		//Act
		result = await ClientTestHelper.GetClientByIdAsync(RequestExecutor, id);

		//Assert
		result.AssertErrors();
		result.MatchSnapshot(opt => opt.IgnoreAllFields("id"));
	}

	[Test]
	[Description("Проверяет получение списка клиентов.")]
	public async Task Getting_clients_list()
	{
		//Arrange
		var fullnames = new List<Fullname>
		{
			new()
			{
				Surname = "default_surname1",
				Name = "default_name1",
				Patronymic = "default_patronymic1"
			},
			new()
			{
				Surname = "default_surname2",
				Name = "default_name2",
				Patronymic = "default_patronymic2"
			}
		};
		foreach(var fullname in fullnames) await ClientTestHelper.CreateClientAsync(RequestExecutor, fullname);

		//Act
		var result = await RequestExecutor.GetClientsAsync();

		//Assert
		result.AssertErrors();
		result.MatchSnapshot(opt => opt.IgnoreAllFields("id"));
	}
}