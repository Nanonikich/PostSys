using System;

namespace PostSys.Dal.Outbox;

/// <summary>Сообщение transactional outbox.</summary>
public sealed class OutboxMessage
{
    /// <summary>Идентификатор.</summary>
    public Guid Id { get; set; }

    /// <summary>Assembly-qualified имя типа доменного события.</summary>
    public string EventType { get; set; } = string.Empty;

    /// <summary>JSON-полезная нагрузка.</summary>
    public string Payload { get; set; } = string.Empty;

    /// <summary>Время возникновения.</summary>
    public DateTimeOffset OccurredAt { get; set; }

    /// <summary>Время успешной обработки (null = pending).</summary>
    public DateTimeOffset? ProcessedAt { get; set; }
}