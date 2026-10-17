using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GraphQL;
using PostSys.Client.Data;
using PostSys.Client.GraphQl;
using PostSys.ReadModels;
using PostSys.ReadModels.Contracts;

namespace PostSys.Client;

/// <summary>Клиент GraphQL для сущности "Посылка".</summary>
public sealed class PackageGraphQlClient(GraphQlConnection connection)
    : GraphQlClientBase(connection), IPackageGraphQlClient
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<Package>> GetPackagesByIdsAsync(
        Guid[] packageIds,
        CancellationToken cancellationToken = default)
    {
        var request = new GraphQLRequest
        {
            Query = @"
            query GetPackages($ids: [UUID!]!) {
                packages(where: { id: { in: $ids } }, take: 30) {
                    items {
                        id
                        address { latitude longitude }
                        dimensions { height length weight }
                        status
                        packageClientId
                        packagePostmanId
                    }
                }
            }",
            Variables = new { ids = packageIds }
        };

        var response = await SendQueryAsync<PackagesResponseModel>(request, cancellationToken);
        return response.Packages.Items;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Package>> GetPackagesByPostmanIdAsync(
        Guid postmanId,
        CancellationToken cancellationToken = default)
    {
        var request = new GraphQLRequest
        {
            Query = @"
            query GetPackages($postmanId: UUID!) {
                packages(take: 30, where: { packagePostmanId: { eq: $postmanId } }, order: { status: ASC, id: ASC }) {
                    items {
                        id
                        address { latitude longitude }
                        dimensions { height length weight }
                        status
                        packageClientId
                    }
                }
            }",
            Variables = new { postmanId }
        };

        var response = await SendQueryAsync<PackagesResponseModel>(request, cancellationToken);
        return response.Packages.Items;
    }

    /// <inheritdoc/>
    public async Task<bool> ChangePackageStatusAsync(
        Guid packageId,
        PackageStatus newStatus,
        CancellationToken cancellationToken = default)
    {
        var request = new GraphQLRequest
        {
            Query = @"
            mutation ChangePackageStatus($input: ChangePackageStatusInput!) {
                changePackageStatus(input: $input) { isSuccess }
            }",
            Variables = new
            {
                input = new
                {
                    id = packageId,
                    status = newStatus.ToString()
                }
            }
        };

        var response = await SendMutationAsync<ChangePackageStatusResponse>(request, cancellationToken);
        return response.ChangePackageStatus.IsSuccess;
    }

    private sealed class PackagesResponseModel
    {
        public PackagesData Packages { get; set; } = null!;
    }

    private sealed class PackagesData
    {
        public List<Package> Items { get; set; } = null!;
    }

    private sealed class ChangePackageStatusResponse
    {
        public ChangePackageStatusData ChangePackageStatus { get; set; } = null!;
    }

    private sealed class ChangePackageStatusData
    {
        public bool IsSuccess { get; set; }
    }
}