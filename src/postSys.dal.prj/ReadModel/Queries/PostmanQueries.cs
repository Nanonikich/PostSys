using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostSys.Application.Abstractions.Queries;
using PostSys.Domain.Abstractions;
using PostSys.ReadModels;

namespace PostSys.Dal.ReadModel.Queries;

/// <summary>Реализация <see cref="IPostmanQueries"/> на <see cref="ReadDbContext"/>.</summary>
public sealed class PostmanQueries(ReadDbContext dbContext, IPasswordHasher passwordHasher) : IPostmanQueries
{
    /// <inheritdoc/>
    public async Task<Postman?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Postman>().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    /// <inheritdoc/>
    public IQueryable<Postman> Query() => dbContext.Set<Postman>();

    /// <inheritdoc/>
    public async Task<Guid?> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrEmpty(password);

        var postman = await dbContext.Set<Postman>()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Email == email, cancellationToken);

        if(postman is null)
            return null;

        return passwordHasher.Verify(postman.Password, password) ? postman.Id : null;
    }
}