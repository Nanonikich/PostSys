using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GraphQL;
using PostSys.Client.Data;
using PostSys.Client.GraphQl;

namespace PostSys.Client;

/// <summary>Клиент GraphQL для сущности "Почтальон".</summary>
public sealed class PostmanGraphQlClient(GraphQlConnection connection)
    : GraphQlClientBase(connection), IPostmanGraphQlClient
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<ReadModels.Postman>> GetPostmenByIdsAsync(
        Guid[] postmanIds,
        CancellationToken cancellationToken = default)
    {
        var request = new GraphQLRequest
        {
            Query = @"
                query GetPostmen($ids: [UUID!]!) {
                    postmen(where: { id: { in: $ids } }, take: 30) {
                        items {
                            id
                            fullname { surname name patronymic }
                            postmanPackageId
                            email
                        }
                    }
                }",
            Variables = new { ids = postmanIds }
        };

        var response = await SendQueryAsync<PostmenByIdsResponseModel>(request, cancellationToken);
        return response.Postmen.Items;
    }

    /// <inheritdoc/>
    public async Task<Contracts.AuthPayload> AuthorizeAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        var request = new GraphQLRequest
        {
            Query = @"
                query AuthorizePostman($email: String!, $password: String!) {
                    authorizePostman(email: $email, password: $password) {
                        postmanId
                        accessToken
                        expiresAt
                    }
                }",
            Variables = new { email, password }
        };

        var response = await SendQueryAsync<AuthorizationPostmanResponseModel>(request, cancellationToken);
        if(response.AuthorizePostman is { PostmanId: var id, AccessToken: { Length: > 0 } } payload
            && id != Guid.Empty)
        {
            return payload;
        }

        throw new GraphQlClientException("Postman not found or no valid token returned.");
    }

    /// <inheritdoc/>
    public async Task<bool> ChangePostmanPackageAsync(
        Guid postmanId,
        Guid packageId,
        CancellationToken cancellationToken = default)
    {
        var request = new GraphQLRequest
        {
            Query = @"
                mutation ChangePostman($postmanId: UUID!, $postmanPackageId: UUID!) {
                    changePostman(input: { id: $postmanId, postmanPackageId: $postmanPackageId }) {
                        isSuccess
                    }
                }",
            Variables = new { postmanId, postmanPackageId = packageId }
        };

        var response = await SendMutationAsync<ChangePostmanResponse>(request, cancellationToken);
        return response.ChangePostman.IsSuccess;
    }

    private sealed class PostmenByIdsResponseModel
    {
        public PostmenByIdsData Postmen { get; set; } = null!;
    }

    private sealed class PostmenByIdsData
    {
        public List<ReadModels.Postman> Items { get; set; } = null!;
    }

    private sealed class AuthorizationPostmanResponseModel
    {
        public Contracts.AuthPayload? AuthorizePostman { get; set; }
    }

    private sealed class ChangePostmanResponse
    {
        public ChangePostmanData ChangePostman { get; set; } = null!;
    }

    private sealed class ChangePostmanData
    {
        public bool IsSuccess { get; set; }
    }
}