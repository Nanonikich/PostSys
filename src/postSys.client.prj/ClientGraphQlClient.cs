using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using GraphQL;

using PostSys.Client.Data;
using PostSys.Client.GraphQl;

namespace PostSys.Client;

/// <summary>Клиент GraphQL для сущности "Клиент".</summary>
public sealed class ClientGraphQlClient(GraphQlConnection connection)
	: GraphQlClientBase(connection), IClientGraphQlClient
{
	/// <inheritdoc/>
	public async Task<IReadOnlyList<ReadModels.Client>> GetClientsByIdsAsync(
		Guid[] clientIds,
		CancellationToken cancellationToken = default)
	{
		var request = new GraphQLRequest
		{
			Query = @"
			query GetClients($ids: [UUID!]!) {
				clients(where: { id: { in: $ids } }, take: 30) {
					items {
						id
						fullname { name patronymic surname }
						phoneNumber
					}
				}
			}",
			Variables = new { ids = clientIds }
		};

		var response = await SendQueryAsync<ClientsByIdsResponseModel>(request, cancellationToken);
		return response.Clients.Items;
	}

	private sealed class ClientsByIdsResponseModel
	{
		public ClientsByIdsData Clients { get; set; } = null!;
	}

	private sealed class ClientsByIdsData
	{
		public List<ReadModels.Client> Items { get; set; } = null!;
	}
}
