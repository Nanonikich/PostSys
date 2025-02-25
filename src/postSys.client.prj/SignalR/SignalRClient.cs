using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR.Client;

using NLog;

using PostSys.Client.Data.SignalR;

namespace PostSys.Client.SignalR;

/// <summary>Клиент SignalR.</summary>
public class SignalRClient : ISignalRClient
{
	#region Static

	private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

	#endregion

	#region Data

	private HubConnection _hubConnection;
	private IDisposable _subscription;

	#endregion

	#region Events

	/// <inheritdoc/>
	public event EventHandler<bool> ChangeServiceStatusEvent;

	#endregion

	#region Methods

	/// <inheritdoc/>
	public Task StartAsync(string url, CancellationToken cancellationToken = default)
	{
		var builder = new HubConnectionBuilder()
			.WithAutomaticReconnect(new RetryPolicyLoop())
			.WithUrl(url + "/notificationHub");

		_hubConnection = builder.Build();
		_subscription = _hubConnection.On<bool>("ReceiveServiceStatus", isAvailable => ChangeServiceStatusEvent?.Invoke(this, isAvailable));
		_hubConnection.Closed += OnConnectionClosedAsync;
		_hubConnection.Reconnecting += OnReconnectingAsync;
		_hubConnection.Reconnected += OnReconnectedAsync;

		IsDisposed = false;

		ConnectWithRetryAsync(_hubConnection, cancellationToken);
		return Task.CompletedTask;
	}

	/// <summary>Подключается к хабу SignalR с переподключением в случае неудачи.</summary>
	/// <param name="connection">Подключение к хабу.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Задача по подключению.</returns>
	private async Task<bool> ConnectWithRetryAsync(HubConnection connection, CancellationToken cancellationToken)
	{
		while(true)
		{
			try
			{
				await connection.StartAsync(cancellationToken);
				ChangeServiceStatusEvent?.Invoke(this, true);
				return true;
			}
			catch when(cancellationToken.IsCancellationRequested)
			{
				return false;
			}
			catch(Exception ex)
			{
				Log.Error(ex, $"Failed to connect to SignalR Hub. Retrying in 10 seconds...");
				ChangeServiceStatusEvent?.Invoke(this, false);
				await Task.Delay(10000, cancellationToken);
			}
		}
	}

	#endregion

	#region Handlers

	private async Task OnConnectionClosedAsync(Exception? exception) => ChangeServiceStatusEvent?.Invoke(this, false);

	private async Task OnReconnectingAsync(Exception? exception) => ChangeServiceStatusEvent?.Invoke(this, false);

	private async Task OnReconnectedAsync(string? connectionId) => ChangeServiceStatusEvent?.Invoke(this, true);

	#endregion

	#region IDisposable

	/// <summary>Возвращает или задаёт флаг уничтожены ли зависимости.</summary>
	/// <value>Флаг уничтожены ли зависимости.</value>
	public bool IsDisposed { get; private set; }

	/// <inheritdoc/>
	public async ValueTask DisposeAsync()
	{
		if(IsDisposed) return;

		await _hubConnection.StopAsync();
		_hubConnection.Closed -= OnConnectionClosedAsync;
		_hubConnection.Reconnecting -= OnReconnectingAsync;
		_hubConnection.Reconnected -= OnReconnectedAsync;
		_subscription?.Dispose();
		await _hubConnection.DisposeAsync();

		IsDisposed = true;
	}

	#endregion
}