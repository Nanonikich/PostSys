using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using HotChocolate.Execution;

using PostSys.IntegrationTests.Api.Clients;
using PostSys.IntegrationTests.Api.Postmen;
using PostSys.IntegrationTests.Common.Extensions;
using PostSys.ReadModels.Contracts;
using PostSys.ReadModels.Helpers;

namespace PostSys.IntegrationTests.Api.Packages;

/// <summary>Вспомогательный класс для тестов сущности "Посылка".</summary>
public class PackageTestHelper
{
	#region Data

	//lang=gql
	private const string GetPackageByIdQuery = @"
											 query ($id: UUID!){
												packageById(id: $id){
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
											 ";

	//lang=gql
	private const string CreatePackageQuery = @"
											mutation ($dimensions: DimensionsInput!, $address: AddressInput!, $status: PackageStatus!, $clientId: UUID!, $postmanId: UUID!){
												createPackage(input: { dimensions: $dimensions, address: $address, status: $status, clientId: $clientId, postmanId: $postmanId }) {
													id
												}
											}
											";

	//lang=gql
	private const string ChangePackageQuery = @"
											 mutation ($id: UUID!, $clientId: UUID!, $postmanId: UUID!){
												changePackage(input: { id: $id, clientId: $clientId, postmanId: $postmanId }) {
													isSuccess
												}
											 }
											 ";

	//lang=gql
	private const string ChangePackageDimensionsQuery = @"
											 mutation ($id: UUID!, $dimensions: DimensionsInput!){
												changePackageDimensions(input: { id: $id, dimensions: $dimensions }) {
													isSuccess
												}
											 }
											 ";

	//lang=gql
	private const string ChangePackageAddressQuery = @"
											 mutation ($id: UUID!, $address: AddressInput!){
												changePackageAddress(input: { id: $id, address: $address }) {
													isSuccess
												}
											 }
											 ";

	//lang=gql
	private const string ChangePackageStatusQuery = @"
											 mutation ($id: UUID!, $status: PackageStatus!){
												changePackageStatus(input: { id: $id, status: $status }) {
													isSuccess
												}
											 }
											 ";

	//lang=gql
	private const string DeletePackageQuery = @"
											 mutation ($id: UUID!){
												deletePackage(input: { id: $id }){
													isSuccess
												}
											 }
											 ";

	#endregion

	#region Methods

	#region Queries

	public static async Task<IExecutionResult> GetPackageByIdAsync(IRequestExecutor executor, Guid id)
	{
		return await executor.ExecuteAsync(
			GetPackageByIdQuery,
			new Dictionary<string, object?>
			{
				{ "id", id }
			});
	}

	#endregion

	#region Mutations

	public static async Task<IExecutionResult> CreatePackageAsync(
		IRequestExecutor executor,
		Dimensions? dimensions = default,
		Address? address = default,
		PackageStatus? status = default)
	{
		dimensions ??= new Dimensions
		{
			Length = 25,
			Height = 60,
			Weight = 82.6
		};
		address ??= new Address
		{
			Longitude = 122,
			Latitude = 160
		};
		status ??= PackageStatus.OnWarehouse;
		var clientId = (await ClientTestHelper.CreateClientAsync(executor))
			.GetIdFromResult<Guid>("createClient");
		var postmanId = (await PostmanTestHelper.CreatePostmanAsync(executor))
			.GetIdFromResult<Guid>("createPostman");

		return await executor.ExecuteAsync(
			CreatePackageQuery,
			new Dictionary<string, object?>
			{
				{ "dimensions", dimensions },
				{ "address", address },
				{ "status", status },
				{ "clientId", clientId },
				{ "postmanId", postmanId }
			});
	}

	public static async Task<IExecutionResult> ChangePackageAsync(
		IRequestExecutor executor, 
		Guid id, 
		Guid clientId,
		Guid postmanId)
	{
		return await executor.ExecuteAsync(
			ChangePackageQuery,
			new Dictionary<string, object?>
			{
				{ "id", id },
				{ "clientId", clientId },
				{ "postmanId", postmanId }
			});
	}

	public static async Task<IExecutionResult> ChangePackageDimensionsAsync(
		IRequestExecutor executor, Guid id, Dimensions dimensions)
	{
		return await executor.ExecuteAsync(
			ChangePackageDimensionsQuery,
			new Dictionary<string, object?>
			{
				{ "id", id },
				{ "dimensions", dimensions }
			});
	}

	public static async Task<IExecutionResult> ChangePackageAddressAsync(
		IRequestExecutor executor, Guid id, Address address)
	{
		return await executor.ExecuteAsync(
			ChangePackageAddressQuery,
			new Dictionary<string, object?>
			{
				{ "id", id },
				{ "address", address }
			});
	}

	public static async Task<IExecutionResult> ChangePackageStatusAsync(
		IRequestExecutor executor, Guid id, PackageStatus status)
	{
		return await executor.ExecuteAsync(
			ChangePackageStatusQuery,
			new Dictionary<string, object?>
			{
				{ "id", id },
				{ "status", status }
			});
	}

	public static async Task<IExecutionResult> DeletePackageAsync(IRequestExecutor executor, Guid id)
	{
		return await executor.ExecuteAsync(
			DeletePackageQuery,
			new Dictionary<string, object?>
			{
				{ "id", id }
			});
	}

	#endregion

	#endregion
}