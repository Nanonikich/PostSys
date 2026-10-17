using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using PostSys.Application.Abstractions.Queries;
using PostSys.Application.Contracts;
using PostSys.ReadModels;

namespace PostSys.Service.Api.Postmen;

/// <summary>Payload авторизации.</summary>
public sealed record AuthPayload(Guid PostmanId, string AccessToken, DateTimeOffset ExpiresAt);

/// <summary>Определяет запросы GraphQL для почтальонов.</summary>
[QueryType]
public static class Queries
{
    /// <summary>Возвращает почтальона по идентификатору.</summary>
    public static Task<Postman?> GetPostmanByIdAsync(
        Guid id,
        [Service] IPostmanQueries queries,
        CancellationToken cancellationToken)
    {
        return queries.GetByIdAsync(id, cancellationToken);
    }

    /// <summary>Возвращает всех почтальонов.</summary>
    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Postman> GetPostmen([Service] IPostmanQueries queries)
    {
        return queries.Query();
    }

    /// <summary>Авторизует почтальона и выдаёт JWT.</summary>
    public static async Task<AuthPayload?> AuthorizePostmanAsync(
        string email,
        string password,
        [Service] IPostmanQueries queries,
        [Service] IJwtTokenService jwtTokenService,
        CancellationToken cancellationToken)
    {
        var postmanId = await queries.AuthenticateAsync(email, password, cancellationToken);
        if(postmanId is null)
            return null;

        var result = jwtTokenService.CreateToken(postmanId.Value, email);
        return new(result.PostmanId, result.AccessToken, result.ExpiresAt);
    }
}