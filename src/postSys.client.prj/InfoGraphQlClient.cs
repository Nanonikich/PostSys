using System;
using System.Threading;
using System.Threading.Tasks;

using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

using PostSys.Client.Data;

namespace PostSys.Client;

/// <summary>Клиент GraphQL для получения информации о сервисе.</summary>
public class InfoGraphQlClient : IInfoGraphQlClient
{
	#region Data

	private readonly string _url;
	private readonly SystemTextJsonSerializer _jsonSerializer = new();

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="InfoGraphQlClient"/>.</summary>
	/// <param name="endpoint">Адрес.</param>
	public InfoGraphQlClient(string endpoint = "http://localhost:5000")
	{
		_url = endpoint + "/graphql";
	}

	#endregion

	#region GetVersion

	/// <inheritdoc/>
	public async Task<string> GetVersionAsync(CancellationToken cancellationToken = default)
	{
		var request = new GraphQLRequest
		{
			Query = @"
				query {
					version
				}"
		};

		try
		{
			using var graphQlHttpClient = new GraphQLHttpClient(_url, _jsonSerializer);
			var response = await graphQlHttpClient.SendQueryAsync<ResponseType>(request, cancellationToken);
			return response.Data?.Version!;
		}
		catch(Exception ex)
		{
			throw new GraphQlClientException("Error during GraphQL request.", ex);
		}
	}

	private class ResponseType
	{
		/// <summary>Возвращает и задает версию сервиса.</summary>
		/// <value>Версия сервиса.</value>
		public string Version { get; set; }
	}

	#endregion
}
