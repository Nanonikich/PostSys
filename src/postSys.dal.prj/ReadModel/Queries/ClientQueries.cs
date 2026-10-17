using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostSys.Application.Abstractions.Queries;
using PostSys.ReadModels;

namespace PostSys.Dal.ReadModel.Queries;

/// <summary>Реализация <see cref="IClientQueries"/> на <see cref="ReadDbContext"/>.</summary>
public sealed class ClientQueries(ReadDbContext dbContext) : IClientQueries
{
    /// <inheritdoc/>
    public async Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Client>().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    /// <inheritdoc/>
    public IQueryable<Client> Query() => dbContext.Set<Client>();
}