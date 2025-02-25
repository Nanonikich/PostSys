using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace PostSys.Dal.Common.UnitOfWork;

/// <summary>Представляет реализацию <see cref="IUnitOfWork"/> на основе <see cref="DbContext"/>.</summary>
/// <typeparam name="TContext">Тип контекста базы данных.</typeparam>
public class UnitOfWork<TContext> : IUnitOfWork, IDbContextTestImpurity where TContext : DbContext
{
	#region Data

	private readonly TContext _dbContext;
	private readonly IDbContextTransaction _transaction;

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="UnitOfWork{TContext}"/>.</summary>
	/// <param name="dbContext">Контекст базы данных.</param>
	/// <exception cref="ArgumentNullException">Значение <paramref name="dbContext"/> не определено.</exception>
	public UnitOfWork(TContext dbContext)
	{
		_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		_transaction = dbContext.Database.BeginTransaction();
	}

	#endregion

	#region Properties

	/// <inheritdoc/>
	DbContext IDbContextTestImpurity.Context => _dbContext;

	#endregion

	#region Methods

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
		await _dbContext.SaveChangesAsync(cancellationToken);
		await _transaction.CommitAsync(cancellationToken);
	}

	/// <summary>Отменяет изменения.</summary>
	public void Rollback() => _transaction.Rollback();

	/// <inheritdoc/>
	public Task RollbackAsync(CancellationToken cancellationToken = default) =>
		_transaction.RollbackAsync(cancellationToken);

	/// <inheritdoc />
	public void Dispose() => _transaction.Dispose();

	#endregion
}