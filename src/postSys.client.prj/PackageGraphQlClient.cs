using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

using PostSys.Client.Data;
using PostSys.ReadModels;
using PostSys.ReadModels.Contracts;

namespace PostSys.Client;

/// <summary>Клиент GraphQL для сущности "Посылка".</summary>
public class PackageGraphQlClient : IPackageGraphQlClient
{
	#region Data

	private readonly string _url;
	private readonly SystemTextJsonSerializer _jsonSerializer = new();

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="PackageGraphQlClient"/>.</summary>
	/// <param name="endpoint">Адрес.</param>
	public PackageGraphQlClient(string endpoint = "http://localhost:5000")
	{
		_url = endpoint + "/graphql";
	}

	#endregion

	#region GetPackagesByIdsAsync

	/// <inheritdoc/>
	public async Task<IReadOnlyList<ReadModels.Package>> GetPackagesByIdsAsync(Guid[] packageIds, CancellationToken cancellationToken = default)
	{
		var query = @"
			query GetPackages($ids: [UUID!]!) {
				packages(where: { id: { in: $ids } }, take: 30) {
					items {
						id
						address {
							latitude
							longitude
						}
						dimensions {
							height
							length
							weight
						}
						status
						packageClientId
						packagePostmanId
					}
				}
			}";

		var request = new GraphQLRequest
		{
			Query = query,
			Variables = new { ids = packageIds }
		};

		using var graphQlHttpClient = new GraphQLHttpClient(_url, _jsonSerializer);
		var response = await graphQlHttpClient.SendQueryAsync<PackagesByIdsResponseModel>(request, cancellationToken);

		if(response.Errors != null && response.Errors.Length != 0)
		{
			var errorMessages = response.Errors.Select(error => error.Message).ToList();
			throw new GraphQlClientException("GraphQL errors occurred: " + string.Join(", ", errorMessages));
		}

		return response.Data.Packages.Items;
	}

	#region Models

	private class PackagesByIdsResponseModel
	{
		/// <summary>Возвращает или задаёт данные, полученные запросом о посылках.</summary>
		/// <value>Данные, полученные запросом о посылках.</value>
		public PackagesByIdsData Packages { get; set; }
	}

	private class PackagesByIdsData
	{
		/// <summary>Возвращает или задаёт список посылок, полученных запросом.</summary>
		/// <value>Список посылок, полученных запросом.</value>
		public List<Package> Items { get; set; }
	}

	#endregion

	#endregion

	#region GetPackagesByPostmanIdAsync

	/// <inheritdoc/>
	public async Task<IReadOnlyList<Package>> GetPackagesByPostmanIdAsync(Guid postmanId, CancellationToken cancellationToken = default)
	{
		var query = @"
			query GetPackages($postmanId: UUID!) {
				packages(take: 30, where: { packagePostmanId: { eq: $postmanId } }, order: { status: ASC, id: ASC }) {
					items {
						id
						address {
							latitude
							longitude
						}
						dimensions {
							height
							length
							weight
						}
						status
						packageClientId
					}
				}
			}";

		var request = new GraphQLRequest
		{
			Query = query,
			Variables = new { postmanId }
		};

		try
		{
			using var graphQlHttpClient = new GraphQLHttpClient(_url, _jsonSerializer);
			var response = await graphQlHttpClient.SendQueryAsync<PackagesByPostmanIdResponseModel>(request, cancellationToken);
			return response.Data.Packages.Items;
		}
		catch(Exception ex)
		{
			throw new GraphQlClientException("Error during GraphQL request.", ex);
		}
	}

	#region Models

	private class PackagesByPostmanIdResponseModel
	{
		/// <summary>Возвращает или задаёт данные, полученные запросом о посылках.</summary>
		/// <value>Данные, полученные запросом о посылках.</value>
		public PackagesByPostmanIdData Packages { get; set; }
	}

	private class PackagesByPostmanIdData
	{
		/// <summary>Возвращает или задаёт список посылок, полученных запросом.</summary>
		/// <value>Список посылок, полученных запросом.</value>
		public List<Package> Items { get; set; }
	}

	#endregion

	#endregion

	#region ChangePackageStatusAsync

	/// <inheritdoc/>
	public async Task<bool> ChangePackageStatusAsync(Guid packageId, PackageStatus newStatus, CancellationToken cancellationToken = default)
	{
		var mutation = @"
			mutation ChangePackageStatus($input: ChangePackageStatusInput!) {
				changePackageStatus(input: $input) {
					isSuccess
				}
			}";

		var request = new GraphQLRequest
		{
			Query = mutation,
			Variables = new
			{
				input = new
				{
					id = packageId,
					status = newStatus.ToString()
				}
			}
		};

		try
		{
			using var graphQlHttpClient = new GraphQLHttpClient(_url, _jsonSerializer);
			var response = await graphQlHttpClient.SendMutationAsync<ChangePackageStatusResponse>(request, cancellationToken);
			return response.Data.ChangePackageStatus.IsSuccess;
		}
		catch(Exception ex)
		{
			throw new GraphQlClientException("Error during GraphQL request.", ex);
		}
	}

	#region Models

	private class ChangePackageStatusResponse
	{
		/// <summary>Возвращает или задаёт данные, полученные запросом об изменении статуса посылки.</summary>
		/// <value>Данные, полученные запросом об изменении статуса посылки.</value>
		public ChangePackageStatusData ChangePackageStatus { get; set; }
	}

	private class ChangePackageStatusData
	{
		/// <summary>Возвращает или задаёт удалось ли изменить статус у посылки.</summary>
		/// <value>Удалось ли изменить статус у посылки.</value>
		public bool IsSuccess { get; set; }
	}

	#endregion

	#endregion
}
