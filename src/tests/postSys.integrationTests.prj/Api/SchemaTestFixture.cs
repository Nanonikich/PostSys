using System.Threading.Tasks;

using HotChocolate.Execution;

using Microsoft.Extensions.DependencyInjection;

using Snapshooter;
using Snapshooter.NUnit;

using PostSys.Dal.ReadModel;
using PostSys.Service.Extensions;

namespace PostSys.IntegrationTests.Api;

/// <summary>Определяет набор тестов для проверки схемы GraphQL.</summary>
[TestFixture]
public class SchemaTestFixture
{
	#region Methods

	[Test]
	[Description("Проверяет схему GraphQL.")]
	public async Task Matching_schema()
	{
		var schema = await new ServiceCollection()
			.AddLogging()
			.AddGraphQl<ReadDbContext>()
			.BuildSchemaAsync();
		var fullName = new NUnitSnapshotFullNameReader().ReadSnapshotFullName();
		var schemaString = schema.Print();
		schemaString.MatchSnapshot(new SnapshotFullName("schema.graphql", fullName.FolderPath));
	}

	#endregion
}