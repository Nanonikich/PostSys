namespace PostSys.Application.Abstractions;

/// <summary>Хранилище исходящих сообщений доменных событий.</summary>
public interface IOutboxStore
{
    /// <summary>Возвращает необработанные сообщения.</summary>
    /// <param name="take">Максимальное число сообщений.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список ожидающих сообщений.</returns>
    Task<IReadOnlyList<OutboxMessageDto>> GetPendingAsync(int take, CancellationToken cancellationToken = default);

    /// <summary>Помечает сообщение обработанным.</summary>
    /// <param name="id">Идентификатор сообщения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача обновления.</returns>
    Task MarkProcessedAsync(Guid id, CancellationToken cancellationToken = default);
}

/// <summary>Сообщение исходящей очереди доменных событий.</summary>
/// <param name="Id">Идентификатор сообщения.</param>
/// <param name="EventType">Полное имя типа события.</param>
/// <param name="Payload">Сериализованное содержимое события.</param>
/// <param name="OccurredAt">Момент возникновения события.</param>
public sealed record OutboxMessageDto(Guid Id, string EventType, string Payload, DateTimeOffset OccurredAt);