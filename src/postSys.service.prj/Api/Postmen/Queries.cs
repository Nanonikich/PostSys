using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;

using PostSys.Application.Abstractions.Queries;
using PostSys.ReadModels;

namespace PostSys.Service.Api.Postmen;

/// <summary>Определяет запросы GraphQL для почтальонов.</summary>
[QueryType]
public static class Queries
{
	#region Methods

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

	/// <summary>Авторизует почтальона по email и паролю.</summary>
	public static Task<Guid?> AuthorizePostmanAsync(
		string email,
		string password,
		[Service] IPostmanQueries queries,
		CancellationToken cancellationToken)
	{
		return queries.AuthenticateAsync(email, password, cancellationToken);
	}

	#endregion
}
