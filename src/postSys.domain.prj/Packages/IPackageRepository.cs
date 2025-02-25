namespace PostSys.Domain.Packages;

/// <summary>Представляет репозиторий для доменных объектов.</summary>
public interface IPackageRepository
{
	#region Methods

	/// <summary>Добавляет посылку.</summary>
	/// <param name="package">Посылка.</param>
	Task AddAsync(Package package);

	/// <summary>Находит и возвращает посылку по идентификатору.</summary>
	/// <param name="id">Идентификатор посылки.</param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
	/// <returns>Посылка, если найден, иначе null.</returns>
	ValueTask<Package?> FindAsync(Guid id, CancellationToken cancellationToken = default);

	#endregion
}