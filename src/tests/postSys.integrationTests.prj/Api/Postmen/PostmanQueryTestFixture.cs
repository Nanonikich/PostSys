using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Snapshooter.NUnit;

using PostSys.IntegrationTests.Api.Extensions;
using PostSys.IntegrationTests.Common.Extensions;
using PostSys.ReadModels.Helpers;

namespace PostSys.IntegrationTests.Api.Postmen;

/// <summary>Представляет набор тестов для запросов.</summary>
[TestFixture]
public class PostmanQueryTestFixture : ApiHandlerTestFixtureBase
{
	[Test]
	[Description("Проверяет получение почтальона по идентификатору.")]
	public async Task Getting_postman_by_id()
	{
		//Arrange
		var result = await PostmanTestHelper.CreatePostmanAsync(RequestExecutor);
		var id = result.GetIdFromResult<Guid>("createPostman");

		//Act
		result = await PostmanTestHelper.GetPostmanByIdAsync(RequestExecutor, id);

		//Assert
		result.AssertErrors();
		result.MatchSnapshot(opt => opt.IgnoreAllFields("id"));
	}

	[Test]
	[Description("Проверяет получение списка почтальонов.")]
	public async Task Getting_postmen_list()
	{
		//Arrange
		const string packageId1 = "c47fdd92-1721-4188-a895-8742d4591535";
		const string packageId2 = "35064716-0ef1-4f85-be26-aa8435b5e187";
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
		await PostmanTestHelper.CreatePostmanAsync(
			RequestExecutor,
			fullnames[0],
			Guid.Parse(packageId1));
		await PostmanTestHelper.CreatePostmanAsync(
			RequestExecutor,
			fullnames[1],
			Guid.Parse(packageId2),
			"nanonano@gmail.com",
			"556622");

		//Act
		var result = await RequestExecutor.GetPostmenAsync();

		//Assert
		result.AssertErrors();
		result.MatchSnapshot(opt => opt.IgnoreAllFields("id"));
	}
}