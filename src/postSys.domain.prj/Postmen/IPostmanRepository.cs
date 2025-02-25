namespace PostSys.Domain.Postmen;

/// <summary>Представляет репозиторий для доменных объектов.</summary>
public interface IPostmanRepository
{
	#region Methods

	/// <summary>Добавляет почтальона.</summary>
	/// <param name="postman">Почтальон.</param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
	Task AddAsync(Postman postman, CancellationToken cancellationToken = default);

	/// <summary>Находит и возвращает почтальона по идентификатору.</summary>
	/// <param name="id">Идентификатор почтальона.</param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
	/// <returns>Почтальон, если найден, иначе null.</returns>
	ValueTask<Postman?> FindAsync(Guid id, CancellationToken cancellationToken = default);

	#endregion
}