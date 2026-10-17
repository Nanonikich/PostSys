using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PostSys.Application.Abstractions;
using PostSys.Domain.Abstractions;

namespace PostSys.Service.Outbox;

/// <summary>Фоновая публикация необработанных сообщений исходящей очереди доменных событий.</summary>
/// <param name="scopeFactory">Фабрика областей внедрения зависимостей.</param>
/// <param name="logger">Журнал событий фоновой службы.</param>
public sealed class OutboxPublisherHostedService(
    IServiceScopeFactory scopeFactory,
    ILogger<OutboxPublisherHostedService> logger) : BackgroundService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await PublishPendingAsync(stoppingToken);
            }
            catch(Exception ex) when(ex is not OperationCanceledException)
            {
                logger.LogError(ex, "Outbox publisher iteration failed.");
            }

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
            catch(OperationCanceledException)
            {
                break;
            }
        }
    }

    private async Task PublishPendingAsync(CancellationToken cancellationToken)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var store = scope.ServiceProvider.GetRequiredService<IOutboxStore>();
        var dispatcher = scope.ServiceProvider.GetRequiredService<IDomainEventDispatcher>();
        var pending = await store.GetPendingAsync(50, cancellationToken);
        if(pending.Count == 0)
            return;

        var events = new List<IDomainEvent>();
        var ids = new List<Guid>();

        foreach(var message in pending)
        {
            var type = Type.GetType(message.EventType, throwOnError: false);
            if(type is null)
            {
                logger.LogWarning("Unknown outbox event type {EventType}, marking processed.", message.EventType);
                await store.MarkProcessedAsync(message.Id, cancellationToken);
                continue;
            }

            var domainEvent = JsonSerializer.Deserialize(message.Payload, type, JsonOptions) as IDomainEvent;
            if(domainEvent is null)
            {
                logger.LogWarning("Failed to deserialize outbox message {Id}.", message.Id);
                await store.MarkProcessedAsync(message.Id, cancellationToken);
                continue;
            }

            events.Add(domainEvent);
            ids.Add(message.Id);
        }

        if(events.Count == 0)
            return;

        await dispatcher.DispatchAsync(events, cancellationToken);
        foreach(var id in ids)
            await store.MarkProcessedAsync(id, cancellationToken);
    }
}