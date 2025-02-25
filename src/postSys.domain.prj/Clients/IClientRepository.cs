namespace PostSys.Domain.Clients;

/// <summary>Представляет репозиторий для доменных объектов.</summary>
public interface IClientRepository
{
	#region Methods

	/// <summary>Добавляет клиента.</summary>
	/// <param name="client">Клиент.</param>
	Task AddAsync(Client client);

	/// <summary>Находит и возвращает клиента по идентификатору.</summary>
	/// <param name="id">Идентификатор клиента.</param>
	/// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
	/// <returns>Клиент, если найден, иначе null.</returns>
	ValueTask<Client?> FindAsync(Guid id, CancellationToken cancellationToken = default);

	#endregion
}