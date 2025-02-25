using System;
using System.Threading;
using System.Threading.Tasks;

using PostSys.Domain.Clients;

namespace PostSys.Dal.Persistence.Clients;

/// <summary>Определяет реализацию репозитория <see cref="IClientRepository"/>.</summary>
/// <param name="writeDbContext">Контекст доступа к данным.</param>
internal class ClientRepository(WriteDbContext writeDbContext) : IClientRepository
{
	#region Methods

	/// <inheritdoc/>
	public async Task AddAsync(Client client)
	{
		await writeDbContext
			.Set<Client>()
			.AddAsync(client);
	}

	/// <inheritdoc/>
	public ValueTask<Client?> FindAsync(Guid id, CancellationToken cancellationToken)
	{
		return writeDbContext.FindAsync<Client>([id], cancellationToken);
	}

	#endregion
}