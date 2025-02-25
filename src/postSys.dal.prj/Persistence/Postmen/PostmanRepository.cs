using System;
using System.Threading;
using System.Threading.Tasks;

using PostSys.Domain.Postmen;

namespace PostSys.Dal.Persistence.Postmen;

/// <summary>Определяет реализацию репозитория <see cref="IPostmanRepository" />.</summary>
/// <param name="writeDbContext">Контекст доступа к данным.</param>
internal class PostmanRepository(WriteDbContext writeDbContext) : IPostmanRepository
{
	#region Methods

	/// <inheritdoc/>
	public async Task AddAsync(Postman postman, CancellationToken cancellationToken)
	{
		await writeDbContext
			.Set<Postman>()
			.AddAsync(postman, cancellationToken);
	}

	/// <inheritdoc/>
	public ValueTask<Postman?> FindAsync(Guid id, CancellationToken cancellationToken)
	{
		return writeDbContext.FindAsync<Postman>([id], cancellationToken);
	}

	#endregion
}