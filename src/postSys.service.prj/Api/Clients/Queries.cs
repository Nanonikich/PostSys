using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;

using PostSys.Dal.ReadModel;
using PostSys.ReadModels;

namespace PostSys.Service.Api.Clients;

/// <summary>Определяет запросы GraphQL для клиентов.</summary>
[QueryType]
public static class Queries
{
	#region Methods

	/// <summary>Возвращает клиента по идентификатору.</summary>
	/// <param name="id">Идентификатор.</param>
	/// <param name="dbContext"><see cref="ReadDbContext"/>.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Клиент, если он существует.</returns>
	[Authorize]
	public static ValueTask<Client?> GetClientByIdAsync(
		Guid id,
		[Service] ReadDbContext dbContext,
		CancellationToken cancellationToken)
	{
		return dbContext
			.Set<Client>()
			.FindAsync([id], cancellationToken);
	}

	/// <summary>Возвращает всех клиентов.</summary>
	/// <param name="dbContext"><see cref="ReadDbContext"/>.</param>
	/// <returns>Все клиенты.</returns>
	[Authorize]
	[UseOffsetPaging]
	[UseFiltering]
	[UseSorting]
	public static IQueryable<Client> GetClients([Service] ReadDbContext dbContext)
	{
		return dbContext.Set<Client>();
	}

	#endregion
}