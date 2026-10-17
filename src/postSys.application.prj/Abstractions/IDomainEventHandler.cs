using PostSys.Domain.Abstractions;

namespace PostSys.Application.Abstractions;

/// <summary>Обработчик доменного события.</summary>
/// <typeparam name="TEvent">Тип доменного события.</typeparam>
public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
{
    /// <summary>Обрабатывает доменное событие.</summary>
    /// <param name="domainEvent">Доменное событие.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken = default);
}