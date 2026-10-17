using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using PostSys.Application.Abstractions.Queries;
using PostSys.ReadModels;

namespace PostSys.Dal.ReadModel.Queries;

/// <summary>Реализация <see cref="IPackageQueries"/> на <see cref="ReadDbContext"/>.</summary>
public sealed class PackageQueries(ReadDbContext dbContext) : IPackageQueries
{
	/// <inheritdoc/>
	public async Task<Package?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return await dbContext.Set<Package>().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
	}

	/// <inheritdoc/>
	public IQueryable<Package> Query() => dbContext.Set<Package>();
}
