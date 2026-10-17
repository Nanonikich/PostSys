using CSharpFunctionalExtensions;

namespace PostSys.Domain.Abstractions;

/// <summary>Базовый класс корня агрегата с поддержкой доменных событий.</summary>
public abstract class AggregateRoot : Entity<Guid>
{
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>Возвращает доменные события, накопленные агрегатом.</summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>Добавляет доменное событие.</summary>
    /// <param name="domainEvent">Событие.</param>
    protected void Raise(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);
        _domainEvents.Add(domainEvent);
    }

    /// <summary>Очищает накопленные доменные события.</summary>
    public void ClearDomainEvents() => _domainEvents.Clear();
}