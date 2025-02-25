using System.Threading.Tasks;

using HotChocolate.Execution;

namespace PostSys.IntegrationTests.Api.Extensions;

public static class PostmanExtensions
{
	//lang=gql
	private const string GetPostmenQuery = @"
						query {
							postmen(take: 10) {
								items {
									id
									fullname {
										surname
										name
										patronymic
									}
									postmanPackageId
									email
									password
								}
							}
						}
						";

	public static async Task<IExecutionResult> GetPostmenAsync(this IRequestExecutor executor) =>
		await executor.ExecuteAsync(GetPostmenQuery);
}