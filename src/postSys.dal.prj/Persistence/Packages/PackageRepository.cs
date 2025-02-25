using System;
using System.Threading;
using System.Threading.Tasks;

using PostSys.Domain.Packages;

namespace PostSys.Dal.Persistence.Packages;

/// <summary>Определяет реализацию репозитория <see cref="IPackageRepository"/>.</summary>
/// <param name="writeDbContext">Контекст доступа к данным.</param>
internal class PackageRepository(WriteDbContext writeDbContext) : IPackageRepository
{
	#region Methods

	/// <inheritdoc/>
	public async Task AddAsync(Package package)
	{
		await writeDbContext
			.Set<Package>()
			.AddAsync(package);
	}

	/// <inheritdoc/>
	public ValueTask<Package?> FindAsync(Guid id, CancellationToken cancellationToken)
	{
		return writeDbContext.FindAsync<Package>([id], cancellationToken);
	}

	#endregion
}