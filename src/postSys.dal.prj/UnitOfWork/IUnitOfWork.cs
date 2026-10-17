using System;
using System.Threading;
using System.Threading.Tasks;

namespace PostSys.Dal.Common.UnitOfWork;

/// <summary>Представляет интерфейс единицы работы с БД.</summary>
public interface IUnitOfWork : IDisposable
{
	#region Methods

	/// <summary>Сохраняет изменения.</summary>
	void Commit();

	/// <summary>Сохраняет изменения.</summary>
	/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
	/// <returns>Асинхронная операция.</returns>
	Task CommitAsync(CancellationToken cancellationToken = default);

	/// <summary>Отменяет изменения.</summary>
	void Rollback();

	/// <summary>Отменяет изменения.</summary>
	/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
	/// <returns>Асинхронная операция.</returns>
	Task RollbackAsync(CancellationToken cancellationToken = default);

	#endregion
}