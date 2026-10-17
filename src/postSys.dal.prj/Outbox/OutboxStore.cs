using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostSys.Application.Abstractions;
using PostSys.Dal.Persistence;

namespace PostSys.Dal.Outbox;

/// <summary>Реализация <see cref="IOutboxStore"/>.</summary>
internal sealed class OutboxStore(WriteDbContext dbContext) : IOutboxStore
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<OutboxMessageDto>> GetPendingAsync(int take, CancellationToken cancellationToken = default)
    {
        var items = await dbContext.OutboxMessages
            .AsNoTracking()
            .Where(x => x.ProcessedAt == null)
            .OrderBy(x => x.OccurredAt)
            .Take(take)
            .Select(x => new OutboxMessageDto(x.Id, x.EventType, x.Payload, x.OccurredAt))
            .ToListAsync(cancellationToken);

        return items;
    }

    /// <inheritdoc/>
    public async Task MarkProcessedAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var message = await dbContext.OutboxMessages.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if(message is null)
            return;

        message.ProcessedAt = DateTimeOffset.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}