using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PostSys.Application.Abstractions;
using PostSys.Dal.Outbox;
using PostSys.Domain.Abstractions;

namespace PostSys.Dal.Common.UnitOfWork;

/// <summary>Представляет реализацию <see cref="IUnitOfWork"/> на основе <see cref="DbContext"/>.</summary>
/// <typeparam name="TContext">Тип контекста базы данных.</typeparam>
public class UnitOfWork<TContext> : IUnitOfWork, IDbContextTestImpurity where TContext : DbContext
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly TContext _dbContext;
    private readonly IDbContextTransaction _transaction;
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private bool _disposed;
    private bool _completed;

    /// <summary>Создаёт экземпляр класса <see cref="UnitOfWork{TContext}"/>.</summary>
    public UnitOfWork(TContext dbContext, IDomainEventDispatcher domainEventDispatcher)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _domainEventDispatcher = domainEventDispatcher ?? throw new ArgumentNullException(nameof(domainEventDispatcher));
        _transaction = dbContext.Database.BeginTransaction();
    }

    /// <inheritdoc/>
    DbContext IDbContextTestImpurity.Context => _dbContext;

    /// <summary>Сохраняет изменения.</summary>
    public void Commit()
    {
        CommitAsync()
            .GetAwaiter()
            .GetResult();
    }

    /// <inheritdoc/>
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        await _dbContext.SaveChangesAsync(cancellationToken);

        var aggregates = _dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(entry => entry.Entity)
            .Where(aggregate => aggregate.DomainEvents.Count > 0)
            .ToList();

        var domainEvents = aggregates
            .SelectMany(aggregate => aggregate.DomainEvents)
            .ToList();

        foreach(var aggregate in aggregates)
            aggregate.ClearDomainEvents();

        var outboxIds = new List<Guid>();
        if(domainEvents.Count > 0)
        {
            var now = DateTimeOffset.UtcNow;
            foreach(var domainEvent in domainEvents)
            {
                var id = Guid.NewGuid();
                outboxIds.Add(id);
                _dbContext.Set<OutboxMessage>().Add(new OutboxMessage
                {
                    Id = id,
                    EventType = domainEvent.GetType().AssemblyQualifiedName
                        ?? domainEvent.GetType().FullName
                        ?? domainEvent.GetType().Name,
                    Payload = JsonSerializer.Serialize(domainEvent, domainEvent.GetType(), JsonOptions),
                    OccurredAt = now
                });
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        await _transaction.CommitAsync(cancellationToken);
        _completed = true;

        if(domainEvents.Count == 0)
            return;

        try
        {
            await _domainEventDispatcher.DispatchAsync(domainEvents, cancellationToken);
            foreach(var id in outboxIds)
            {
                var message = await _dbContext.Set<OutboxMessage>()
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
                if(message is not null)
                    message.ProcessedAt = DateTimeOffset.UtcNow;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            // Сообщения остаются pending — OutboxPublisher повторит доставку.
        }
    }

    /// <summary>Отменяет изменения.</summary>
    public void Rollback() =>
        RollbackAsync()
            .GetAwaiter()
            .GetResult();

    /// <inheritdoc/>
    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if(_disposed || _completed)
            return;

        await _transaction.RollbackAsync(cancellationToken);
        _completed = true;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        DisposeAsync()
            .AsTask()
            .GetAwaiter()
            .GetResult();
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        if(_disposed)
            return;

        if(!_completed)
        {
            try
            {
                await _transaction.RollbackAsync();
            }
            catch
            {
                // Транзакция могла быть уже завершена провайдером.
            }
        }

        await _transaction.DisposeAsync();
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}