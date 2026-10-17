using PostSys.Domain.Abstractions;

namespace PostSys.Application.Abstractions;

/// <summary>Диспетчер доменных событий.</summary>
public interface IDomainEventDispatcher
{
    /// <summary>Отправляет доменные события на обработку.</summary>
    /// <param name="domainEvents">Доменные события.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
}