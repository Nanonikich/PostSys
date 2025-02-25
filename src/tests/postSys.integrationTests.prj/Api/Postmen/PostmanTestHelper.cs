using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using HotChocolate.Execution;

using PostSys.ReadModels.Helpers;

namespace PostSys.IntegrationTests.Api.Postmen;

/// <summary>Вспомогательный класс для тестов сущности "Почтальон".</summary>
public class PostmanTestHelper
{
	#region Data

	//lang=gql
	private const string GetPostmenByIdQuery = @"
											 query ($id: UUID!){
												postmanById (id: $id){
													fullname {
														surname
														name
														patronymic
													}
													email
													password
												}
											 }
											 ";

	//lang=gql
	private const string CreatePostmanQuery = @"
											mutation ($fullname: FullnameInput!, $packageId: UUID!, $email: String!, $password: String!){
												createPostman(input: { fullname: $fullname, packageId: $packageId, email: $email, password: $password }) {
													id
												}
											}
											";

	//lang=gql
	private const string ChangePostmanFullnameQuery = @"
											 mutation ($id: UUID!, $fullname: FullnameInput!){
												changePostmanFullname(input: { id: $id, fullname: $fullname }){
													isSuccess
												}
											 }
											 ";

	//lang=gql
	private const string ChangePostmanEmailQuery = @"
											 mutation ($id: UUID!, $email: String!){
												changePostmanEmail(input: { id: $id, email: $email }){
													isSuccess
												}
											 }
											 ";

	//lang=gql
	private const string ChangePostmanPasswordQuery = @"
											 mutation ($id: UUID!, $password: String!){
												changePostmanPassword(input: { id: $id, password: $password }){
													isSuccess
												}
											 }
											 ";

	//lang=gql
	private const string DeletePostmanQuery = @"
											 mutation ($id: UUID!){
												deletePostman(input: { id: $id }){
													isSuccess
												}
											 }
											 ";

	#endregion

	#region Methods

	#region Queries

	public static async Task<IExecutionResult> GetPostmanByIdAsync(IRequestExecutor executor, Guid id)
	{
		return await executor.ExecuteAsync(
			GetPostmenByIdQuery,
			new Dictionary<string, object?>
			{
				{ "id", id }
			});
	}

	#endregion

	#region Mutations

	public static async Task<IExecutionResult> CreatePostmanAsync(
		IRequestExecutor executor,
		Fullname? fullname = default,
		Guid packageId = default,
		string email = "nano@gmail.com",
		string password = "555555")
	{
		fullname ??= new Fullname
		{
			Surname = "default_surname",
			Name = "default_name",
			Patronymic = "default_patronymic"
		};
		return await executor.ExecuteAsync(
			CreatePostmanQuery,
			new Dictionary<string, object?>
			{
				{ "fullname", fullname },
				{ "packageId", packageId },
				{ "email", email },
				{ "password", password }
			});
	}

	public static async Task<IExecutionResult> ChangePostmanFullnameAsync(
		IRequestExecutor executor, Guid id, Fullname fullname)
	{
		return await executor.ExecuteAsync(
			ChangePostmanFullnameQuery,
			new Dictionary<string, object?>
			{
				{ "id", id },
				{ "fullname", fullname }
			});
	}

	public static async Task<IExecutionResult> ChangePostmanEmailAsync(IRequestExecutor executor, Guid id, string email)
	{
		return await executor.ExecuteAsync(
			ChangePostmanEmailQuery,
			new Dictionary<string, object?>
			{
				{ "id", id },
				{ "email", email }
			});
	}

	public static async Task<IExecutionResult> ChangePostmanPasswordAsync(
		IRequestExecutor executor, Guid id, string password)
	{
		return await executor.ExecuteAsync(
			ChangePostmanPasswordQuery,
			new Dictionary<string, object?>
			{
				{ "id", id },
				{ "password", password }
			});
	}

	public static async Task<IExecutionResult> DeletePostmanAsync(IRequestExecutor executor, Guid id)
	{
		return await executor.ExecuteAsync(
			DeletePostmanQuery,
			new Dictionary<string, object?>
			{
				{ "id", id }
			});
	}

	#endregion

	#endregion
}