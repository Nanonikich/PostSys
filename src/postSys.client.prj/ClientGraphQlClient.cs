using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

using PostSys.Client.Data;

namespace PostSys.Client;

/// <summary>Клиент GraphQL для сущности "Клиент".</summary>
public class ClientGraphQlClient : IClientGraphQlClient
{
	#region Data

	private readonly string _url;
	private readonly SystemTextJsonSerializer _jsonSerializer = new();

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="ClientGraphQlClient"/>.</summary>
	/// <param name="endpoint">Адрес.</param>
	public ClientGraphQlClient(string endpoint = "http://localhost:5000")
	{
		_url = endpoint + "/graphql";
	}

	#endregion

	#region GetClientsByIdsAsync

	/// <inheritdoc/>
	public async Task<IReadOnlyList<ReadModels.Client>> GetClientsByIdsAsync(Guid[] clientIds, CancellationToken cancellationToken = default)
	{
		var query = @"
			query GetClients($ids: [UUID!]!) {
				clients(where: { id: { in: $ids } }, take: 30) {
					items {
						id
						fullname {
							name
							patronymic
							surname
						}
						phoneNumber
					}
				}
			}";

		var request = new GraphQLRequest
		{
			Query = query,
			Variables = new { ids = clientIds }
		};

		using var graphQlHttpClient = new GraphQLHttpClient(_url, _jsonSerializer);
		var response = await graphQlHttpClient.SendQueryAsync<ClientsByIdsResponseModel>(request, cancellationToken);

		if(response.Errors != null && response.Errors.Length != 0)
		{
			var errorMessages = response.Errors.Select(error => error.Message).ToList();
			throw new GraphQlClientException("GraphQL errors occurred: " + string.Join(", ", errorMessages));
		}

		return response.Data.Clients.Items;
	}

	#region Models

	private class ClientsByIdsResponseModel
	{
		/// <summary>Возвращает или задаёт данные, полученные запросом о клиентах.</summary>
		/// <value>Данные, полученные запросом о клиентах.</value>
		public ClientsByIdsData Clients { get; set; }
	}

	private class ClientsByIdsData
	{
		/// <summary>Возвращает или задаёт список клиентов, полученных запросом.</summary>
		/// <value>Список клиентов, полученных запросом.</value>
		public List<ReadModels.Client> Items { get; set; }
	}

	#endregion

	#endregion
}
