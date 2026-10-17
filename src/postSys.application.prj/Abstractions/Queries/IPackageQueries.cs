namespace PostSys.Application.Abstractions.Queries;

/// <summary>Порт запросов посылок.</summary>
public interface IPackageQueries
{
	/// <summary>Возвращает посылку по идентификатору.</summary>
	/// <param name="id">Идентификатор посылки.</param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
	Task<PostSys.ReadModels.Package?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

	/// <summary>Возвращает запрос для чтения посылок.</summary>
	IQueryable<PostSys.ReadModels.Package> Query();
}
