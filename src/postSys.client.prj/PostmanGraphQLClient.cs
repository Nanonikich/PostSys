using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

using PostSys.Client.Data;

namespace PostSys.Client;

/// <summary>Клиент GraphQL для сущности "Почтальон".</summary>
public class PostmanGraphQlClient : IPostmanGraphQlClient
{
	#region Data

	private readonly string _url;
	private readonly SystemTextJsonSerializer _jsonSerializer = new();

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="PostmanGraphQlClient"/>.</summary>
	/// <param name="endpoint">Адрес.</param>
	public PostmanGraphQlClient(string endpoint = "http://localhost:5000")
	{
		_url = endpoint + "/graphql";
	}

	#endregion

	#region GetPostmenByIdsAsync

	/// <inheritdoc/>
	public async Task<IReadOnlyList<ReadModels.Postman>> GetPostmenByIdsAsync(Guid[] postmanIds, CancellationToken cancellationToken = default)
	{
		var query = @"
				query GetPostmen($ids: [UUID!]!) {
					postmen(where: { id: { in: $ids } }, take: 30) {
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
				}";

		var request = new GraphQLRequest
		{
			Query = query,
			Variables = new { ids = postmanIds }
		};

		using var graphQlHttpClient = new GraphQLHttpClient(_url, _jsonSerializer);
		var response = await graphQlHttpClient.SendQueryAsync<PostmenByIdsResponseModel>(request, cancellationToken);

		if(response.Errors != null && response.Errors.Length != 0)
		{
			var errorMessages = response.Errors.Select(error => error.Message).ToList();
			throw new GraphQlClientException("GraphQL errors occurred: " + string.Join(", ", errorMessages));
		}

		return response.Data.Postmen.Items;
	}

	#region Models

	private class PostmenByIdsResponseModel
	{
		/// <summary>Возвращает или задаёт данные, полученные запросом о почтальонах.</summary>
		/// <value>Данные, полученные запросом о почтальонах.</value>
		public PostmenByIdsData Postmen { get; set; }
	}

	private class PostmenByIdsData
	{
		/// <summary>Возвращает или задаёт список почтальонов, полученных запросом.</summary>
		/// <value>Список почтальонов, полученных запросом.</value>
		public List<ReadModels.Postman> Items { get; set; }
	}

	#endregion

	#endregion

	#region GetAuthorizationPostmanIdAsync

	/// <inheritdoc/>
	public async Task<Guid> GetAuthorizationPostmanIdAsync(string email, string password, CancellationToken cancellationToken = default)
	{
		var query = new GraphQLRequest
		{
			Query = @"
				query GetPostman($email: String!, $password: String!) {
					postmen(
						where: { email: { eq: $email }, password: { eq: $password } }
						take: 1
					) {
						items {
							id
						}
					}
				}",
			Variables = new { email, password }
		};

		try
		{
			using var graphQlHttpClient = new GraphQLHttpClient(_url, _jsonSerializer);
			var response = await graphQlHttpClient.SendQueryAsync<AuthorizationPostmanResponseModel>(query, cancellationToken);
			return ParseGetAuthorizationPostmanResult(response);
		}
		catch(Exception ex)
		{
			throw new GraphQlClientException("Error during GraphQL request.", ex);
		}
	}

	private Guid ParseGetAuthorizationPostmanResult(GraphQLResponse<AuthorizationPostmanResponseModel> response)
	{
		var items = response.Data.Postmen.Items;
		if(items.Count > 0)
		{
			return items[0].Id;
		}
		throw new GraphQlClientException("Postman not found or no valid ID returned.");
	}

	#region Models

	private class AuthorizationPostmanResponseModel
	{
		/// <summary>Возвращает или задаёт данные, полученные запросом о почтальонах.</summary>
		/// <value>Данные, полученные запросом о почтальонах.</value>
		public AuthorizationPostmanData Postmen { get; set; }
	}

	private class AuthorizationPostmanData
	{
		/// <summary>Возвращает или задаёт список почтальонов, полученных запросом.</summary>
		/// <value>Список почтальоном, полученных запросом.</value>
		public List<AuthorizationPostman> Items { get; set; }
	}

	private class AuthorizationPostman
	{
		/// <summary>Возвращает или задаёт идентификатор почтальона.</summary>
		/// <value>Идентификатор почтальона.</value>
		public Guid Id { get; set; }
	}

	#endregion

	#endregion

	#region ChangePostmanPackageAsync

	/// <inheritdoc/>
	public async Task<bool> ChangePostmanPackageAsync(Guid postmanId, Guid packageId, CancellationToken cancellationToken = default)
	{
		var mutation = new GraphQLRequest
		{
			Query = @"
				mutation ChangePostman($postmanId: UUID!, $postmanPackageId: UUID!) {
					changePostman(input: { id: $postmanId, postmanPackageId: $postmanPackageId }) {
						isSuccess
					}
				}",
			Variables = new { postmanId = postmanId, postmanPackageId = packageId }
		};

		try
		{
			using var graphQlHttpClient = new GraphQLHttpClient(_url, _jsonSerializer);
			var response = await graphQlHttpClient.SendMutationAsync<ChangePostmanResponse>(mutation, cancellationToken);
			return response.Data.ChangePostman.IsSuccess;
		}
		catch(Exception ex)
		{
			throw new GraphQlClientException("Error during GraphQL request.", ex);
		}
	}

	#region Models

	private class ChangePostmanResponse
	{
		/// <summary>Возвращает или задаёт данные, полученные запросом замены посылки у почтальона.</summary>
		/// <value>Данные, полученные запросом замены посылки у почтальона.</value>
		public ChangePostmanData ChangePostman { get; set; }
	}

	private class ChangePostmanData
	{
		/// <summary>Возвращает или задаёт удалось ли заменить в системе посылку у почтальона.</summary>
		/// <value>Удалось ли заменить в системе посылку у почтальона.</value>
		public bool IsSuccess { get; set; }
	}

	#endregion

	#endregion
}