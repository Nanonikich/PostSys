using System;
using System.Threading;
using System.Threading.Tasks;

namespace PostSys.Framework.Common;

/// <summary> Позволяет блокировать исполнение определённого кода на основе применения <see cref="SemaphoreSlim"/>.</summary>
public sealed class SemaphoreLocker
{
	#region Data

	private readonly SemaphoreSlim _semaphore = new(1, 1);

	#endregion

	#region Methods

	/// <summary>
	/// Гарантирует, что асинхронная задача action выполнится только тогда, 
	/// когда никакой другой код не будет исполняться с применением данного экземпляра.
	/// </summary>
	/// <param name="action">
	/// Асинхронное действие, которое должно быть выполнено изолировано от другого кода,
	/// который может исполняться с применением данного экземпляра.
	/// </param>
	/// <returns>Асинхронная операция по исполнению действия изолировано.</returns>
	public Task LockAsync(Func<Task> action) => LockAsync(action, default);

	/// <summary>
	/// Гарантирует, что асинхронная задача action выполнится только тогда, когда никакой
	/// другой код не будет исполняться с применением данного экземпляра.
	/// </summary>
	/// <param name="action">
	/// Асинхронное действие, которое должно быть выполнено изолировано от другого кода,
	/// который может исполняться с применением данного экземпляра.
	/// </param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Асинхронная операция по исполнению действия изолировано.</returns>
	public async Task LockAsync(Func<Task> action, CancellationToken cancellationToken)
	{
		await _semaphore.WaitAsync(cancellationToken);
		try
		{
			await action();
		}
		finally
		{
			_semaphore.Release();
		}
	}

	/// <summary>
	/// Гарантирует, что асинхронная функция func выполнится только тогда, когда никакой
	/// другой код не будет исполняться с применением данного экземпляра.
	/// </summary>
	/// <typeparam name="TResult">Тип результата выполнения функции.</typeparam>
	/// <param name="func">
	/// Функция, которая должна быть выполнена изолировано и асинхронно от другого кода,
	/// который может исполняться с применением данного экземпляра.
	/// </param>
	/// <returns>Задача по получению результата асинхронного и изолированного выполнения функции.</returns>
	public Task<TResult> LockAsync<TResult>(Func<Task<TResult>> func) => LockAsync(func, default);

	/// <summary>
	/// Гарантирует, что асинхронная функция func выполнится только тогда, когда никакой
	/// другой код не будет исполняться с применением данного экземпляра.
	/// </summary>
	/// <typeparam name="TResult">Тип результата выполнения функции.</typeparam>
	/// <param name="func">
	/// Функция, которая должна быть выполнена изолировано и асинхронно от другого кода,
	/// который может исполняться с применением данного экземпляра.
	/// </param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Задача по получению результата асинхронного и изолированного выполнения функции.</returns>
	public async Task<TResult> LockAsync<TResult>(Func<Task<TResult>> func, CancellationToken cancellationToken)
	{
		await _semaphore.WaitAsync(cancellationToken);
		try
		{
			return await func();
		}
		finally
		{
			_semaphore.Release();
		}
	}

	#endregion
}
