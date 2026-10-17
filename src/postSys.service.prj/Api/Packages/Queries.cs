using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using PostSys.Application.Abstractions.Queries;
using PostSys.ReadModels;

namespace PostSys.Service.Api.Packages;

/// <summary>Определяет запросы GraphQL для посылок.</summary>
[QueryType]
public static class Queries
{
    /// <summary>Возвращает посылку по идентификатору.</summary>
    public static Task<Package?> GetPackageByIdAsync(
        Guid id,
        [Service] IPackageQueries queries,
        CancellationToken cancellationToken)
    {
        return queries.GetByIdAsync(id, cancellationToken);
    }

    /// <summary>Возвращает все посылки.</summary>
    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Package> GetPackages([Service] IPackageQueries queries)
    {
        return queries.Query();
    }
}