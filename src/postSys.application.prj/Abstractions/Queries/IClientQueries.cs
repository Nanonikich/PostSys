namespace PostSys.Application.Abstractions.Queries;

/// <summary>Порт запросов клиентов.</summary>
public interface IClientQueries
{
    /// <summary>Возвращает клиента по идентификатору.</summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<PostSys.ReadModels.Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>Возвращает запрос для чтения клиентов.</summary>
    IQueryable<PostSys.ReadModels.Client> Query();
}