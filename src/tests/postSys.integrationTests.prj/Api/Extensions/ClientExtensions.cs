using System.Threading.Tasks;

using HotChocolate.Execution;

namespace PostSys.IntegrationTests.Api.Extensions;

public static class ClientExtensions
{
	//lang=gql
	private const string GetClientsQuery = @"
						query {
							clients(take: 10) {
								items {
									id
									fullname {
										surname
										name
										patronymic
									}
									phoneNumber
								}
							}
						}
						";

	public static async Task<IExecutionResult> GetClientsAsync(this IRequestExecutor executor) => 
		await executor.ExecuteAsync(GetClientsQuery);
}