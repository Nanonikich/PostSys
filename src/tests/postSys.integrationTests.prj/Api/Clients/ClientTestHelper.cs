using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotChocolate.Execution;
using PostSys.ReadModels.Helpers;
using PostSys.IntegrationTests.Api.Extensions;

namespace PostSys.IntegrationTests.Api.Clients;

/// <summary>Вспомогательный класс для тестов сущности "Клиент".</summary>
public class ClientTestHelper
{
    #region Data

    //lang=gql
    private const string GetClientsByIdQuery = @"
                                             query ($id: UUID!){
                                                clientById (id: $id){
                                                    fullname {
                                                        surname
                                                        name
                                                        patronymic
                                                    }
                                                    phoneNumber
                                                }
                                             }
                                             ";

    //lang=gql
    private const string CreateClientQuery = @"
                                            mutation ($fullname: FullnameInput!, $phoneNumber: String!) {
                                                createClient(input: { fullname: $fullname, phoneNumber: $phoneNumber }) {
                                                    id
                                                }
                                            }
                                            ";

    //lang=gql
    private const string ChangeClientFullnameQuery = @"
                                             mutation ($id: UUID!, $fullname: FullnameInput!){
                                                changeClientFullname(input: { id: $id, fullname: $fullname }){
                                                    isSuccess
                                                }
                                             }
                                             ";

    //lang=gql
    private const string ChangeClientPhoneNumberQuery = @"
                                             mutation ($id: UUID!, $phoneNumber: String!){
                                                changeClientPhoneNumber(input: { id: $id, phoneNumber: $phoneNumber }){
                                                    isSuccess
                                                }
                                             }
                                             ";

    //lang=gql
    private const string DeleteClientQuery = @"
                                             mutation ($id: UUID!){
                                                deleteClient(input: { id: $id }){
                                                    isSuccess
                                                }
                                             }
                                             ";

    #endregion

    #region Methods

    #region Queries

    public static async Task<IExecutionResult> GetClientByIdAsync(IRequestExecutor executor, Guid id)
    {
        return await GraphQlTestExecute.ExecuteAsync(executor, 
            GetClientsByIdQuery,
            new Dictionary<string, object?>
            {
                {
                    "id", id
                }
            });
    }

    #endregion

    #region Mutations

    public static async Task<IExecutionResult> CreateClientAsync(IRequestExecutor executor,
                                                                 Fullname? fullname = default,
                                                                 string phoneNumber = "8906-548-22-68")
    {
        fullname = fullname ?? new Fullname
        {
            Surname = "default_surname",
            Name = "default_name",
            Patronymic = "default_patronymic"
        };
        return await GraphQlTestExecute.ExecuteAsync(executor, 
            CreateClientQuery,
            new Dictionary<string, object?>
            {
                { "fullname", fullname },
                { "phoneNumber", phoneNumber }
            });
    }

    public static async Task<IExecutionResult> ChangeClientFullnameAsync(
        IRequestExecutor executor, Guid id, Fullname fullname)
    {
        return await GraphQlTestExecute.ExecuteAsync(executor, 
            ChangeClientFullnameQuery,
            new Dictionary<string, object?>
            {
                { "id", id },
                { "fullname", fullname }
            });
    }

    public static async Task<IExecutionResult> ChangeClientPhoneNumberAsync(
        IRequestExecutor executor, Guid id, string phoneNumber)
    {
        return await GraphQlTestExecute.ExecuteAsync(executor, 
            ChangeClientPhoneNumberQuery,
            new Dictionary<string, object?>
            {
                { "id", id },
                { "phoneNumber", phoneNumber }
            });
    }

    public static async Task<IExecutionResult> DeleteClientAsync(IRequestExecutor executor, Guid id)
    {
        return await GraphQlTestExecute.ExecuteAsync(executor, 
            DeleteClientQuery,
            new Dictionary<string, object?>
            {
                { "id", id }
            });
    }

    #endregion

    #endregion
}