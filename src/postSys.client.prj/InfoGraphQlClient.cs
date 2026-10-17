using System.Threading;
using System.Threading.Tasks;

using GraphQL;

using PostSys.Client.Data;
using PostSys.Client.GraphQl;

namespace PostSys.Client;

/// <summary>Клиент GraphQL для получения информации о сервисе.</summary>
public sealed class InfoGraphQlClient(GraphQlConnection connection)
	: GraphQlClientBase(connection), IInfoGraphQlClient
{
	/// <inheritdoc/>
	public async Task<string> GetVersionAsync(CancellationToken cancellationToken = default)
	{
		var request = new GraphQLRequest
		{
			Query = @"query { version }"
		};

		var response = await SendQueryAsync<ResponseType>(request, cancellationToken);
		return response.Version;
	}

	private sealed class ResponseType
	{
		public string Version { get; set; } = null!;
	}
}
