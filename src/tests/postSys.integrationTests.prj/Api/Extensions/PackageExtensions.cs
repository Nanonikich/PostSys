using System.Threading.Tasks;

using HotChocolate.Execution;

namespace PostSys.IntegrationTests.Api.Extensions;

public static class PackageExtensions
{
	//lang=gql
	private const string GetPackagesQuery = @"
						query {
							packages(take: 10) {
								items {
									id
									dimensions {
										length
										height
										weight
									}
									address {
										longitude
										latitude
									}
									status
									packageClientId
									packagePostmanId
								}
							}
						}
						";

	public static async Task<IExecutionResult> GetPackagesAsync(this IRequestExecutor executor) =>
		await executor.ExecuteAsync(GetPackagesQuery);
}