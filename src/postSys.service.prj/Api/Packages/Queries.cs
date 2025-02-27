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

namespace PostSys.Service.Api.Packages;

/// <summary>Определяет запросы GraphQL для посылок.</summary>
[QueryType]
public static class Queries
{
	#region Methods

	/// <summary>Возвращает посылку по идентификатору.</summary>
	/// <param name="id">Идентификатор.</param>
	/// <param name="dbContext"><see cref="ReadDbContext"/>.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Посылка, если она существует.</returns>
	[Authorize]
	public static ValueTask<Package?> GetPackageByIdAsync(
		Guid id,
		[Service] ReadDbContext dbContext,
		CancellationToken cancellationToken)
	{
		return dbContext
			.Set<Package>()
			.FindAsync([id], cancellationToken);
	}

	/// <summary>Возвращает все посылки.</summary>
	/// <param name="dbContext"><see cref="ReadDbContext"/>.</param>
	/// <returns>Все посылки.</returns>
	[Authorize]
	[UseOffsetPaging]
	[UseFiltering]
	[UseSorting]
	public static IQueryable<Package> GetPackages([Service] ReadDbContext dbContext)
	{
		return dbContext.Set<Package>();
	}

	#endregion
}