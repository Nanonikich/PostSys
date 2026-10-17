using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;

using PostSys.Application.Abstractions.Queries;
using PostSys.ReadModels;

namespace PostSys.Service.Api.Clients;

/// <summary>Определяет запросы GraphQL для клиентов.</summary>
[QueryType]
public static class Queries
{
	#region Methods

	/// <summary>Возвращает клиента по идентификатору.</summary>
	public static Task<Client?> GetClientByIdAsync(
		Guid id,
		[Service] IClientQueries queries,
		CancellationToken cancellationToken)
	{
		return queries.GetByIdAsync(id, cancellationToken);
	}

	/// <summary>Возвращает всех клиентов.</summary>
	[UseOffsetPaging]
	[UseFiltering]
	[UseSorting]
	public static IQueryable<Client> GetClients([Service] IClientQueries queries)
	{
		return queries.Query();
	}

	#endregion
}
