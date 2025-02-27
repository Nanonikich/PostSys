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

namespace PostSys.Service.Api.Postmen;

/// <summary>Определяет запросы GraphQL для почтальонов.</summary>
[QueryType]
public static class Queries
{
	#region Methods

	/// <summary>Возвращает почтальона по идентификатору.</summary>
	/// <param name="id">Идентификатор.</param>
	/// <param name="dbContext"><see cref="ReadDbContext"/>.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Почтальон, если он существует.</returns>
	[Authorize]
	public static ValueTask<Postman?> GetPostmanByIdAsync(
		Guid id,
		[Service] ReadDbContext dbContext,
		CancellationToken cancellationToken)
	{
		return dbContext
			.Set<Postman>()
			.FindAsync([id], cancellationToken);
	}

	/// <summary>Возвращает всех почтальонов.</summary>
	/// <param name="dbContext"><see cref="ReadDbContext"/>.</param>
	/// <returns>Все почтальоны.</returns>
	[Authorize]
	[UseOffsetPaging]
	[UseFiltering]
	[UseSorting]
	public static IQueryable<Postman> GetPostmen([Service] ReadDbContext dbContext)
	{
		return dbContext.Set<Postman>();
	}

	#endregion
}