using System;
using System.Threading;
using System.Threading.Tasks;

namespace PostSys.Client.Data.SignalR;

/// <summary>Клиент SignalR.</summary>
public interface ISignalRClient : IAsyncDisposable
{
	#region Events

	/// <summary>Событие изменения состояния сервиса.</summary>
	event EventHandler<bool> ChangeServiceStatusEvent;

	#endregion

	#region Methods

	/// <summary>Запуск подключения к сервису по SignalR.</summary>
	/// <param name="url">Адрес.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Асинхронная операция.</returns>
	Task StartAsync(string url, CancellationToken cancellationToken = default);

	#endregion
}

