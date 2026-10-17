using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR.Client;

using NLog;

using PostSys.Client.Data.SignalR;

namespace PostSys.Client.SignalR;

/// <summary>Клиент SignalR.</summary>
public sealed class SignalRClient : ISignalRClient
{
	private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

	private readonly SemaphoreSlim _gate = new(1, 1);
	private HubConnection? _hubConnection;
	private IDisposable? _subscription;
	private CancellationTokenSource? _connectCts;
	private bool _disposed;

	/// <inheritdoc/>
	public event EventHandler<bool>? ChangeServiceStatusEvent;

	/// <inheritdoc/>
	public async Task StartAsync(string url, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(url);
		ObjectDisposedException.ThrowIf(_disposed, this);

		await _gate.WaitAsync(cancellationToken);
		try
		{
			await StopCoreAsync();

			_connectCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			var connectToken = _connectCts.Token;

			var builder = new HubConnectionBuilder()
				.WithAutomaticReconnect(new RetryPolicyLoop())
				.WithUrl(url.TrimEnd('/') + "/notificationHub");

			_hubConnection = builder.Build();
			_subscription = _hubConnection.On<bool>("ReceiveServiceStatus", isAvailable =>
				ChangeServiceStatusEvent?.Invoke(this, isAvailable));
			_hubConnection.Closed += OnConnectionClosedAsync;
			_hubConnection.Reconnecting += OnReconnectingAsync;
			_hubConnection.Reconnected += OnReconnectedAsync;

			_ = ConnectWithRetryAsync(_hubConnection, connectToken);
		}
		finally
		{
			_gate.Release();
		}
	}

	/// <inheritdoc/>
	public Task RestartAsync(string url, CancellationToken cancellationToken = default)
		=> StartAsync(url, cancellationToken);

	private async Task<bool> ConnectWithRetryAsync(HubConnection connection, CancellationToken cancellationToken)
	{
		while(!cancellationToken.IsCancellationRequested)
		{
			try
			{
				await connection.StartAsync(cancellationToken);
				ChangeServiceStatusEvent?.Invoke(this, true);
				return true;
			}
			catch(OperationCanceledException) when(cancellationToken.IsCancellationRequested)
			{
				return false;
			}
			catch(Exception ex)
			{
				Log.Error(ex, "Failed to connect to SignalR Hub. Retrying in 10 seconds...");
				ChangeServiceStatusEvent?.Invoke(this, false);
				try
				{
					await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
				}
				catch(OperationCanceledException)
				{
					return false;
				}
			}
		}

		return false;
	}

	private Task OnConnectionClosedAsync(Exception? exception)
	{
		ChangeServiceStatusEvent?.Invoke(this, false);
		return Task.CompletedTask;
	}

	private Task OnReconnectingAsync(Exception? exception)
	{
		ChangeServiceStatusEvent?.Invoke(this, false);
		return Task.CompletedTask;
	}

	private Task OnReconnectedAsync(string? connectionId)
	{
		ChangeServiceStatusEvent?.Invoke(this, true);
		return Task.CompletedTask;
	}

	private async Task StopCoreAsync()
	{
		if(_connectCts is not null)
		{
			try
			{
				await _connectCts.CancelAsync();
			}
			catch
			{
				// ignore
			}

			_connectCts.Dispose();
			_connectCts = null;
		}

		if(_hubConnection is null)
			return;

		_hubConnection.Closed -= OnConnectionClosedAsync;
		_hubConnection.Reconnecting -= OnReconnectingAsync;
		_hubConnection.Reconnected -= OnReconnectedAsync;
		_subscription?.Dispose();
		_subscription = null;

		try
		{
			await _hubConnection.StopAsync();
		}
		catch(Exception ex)
		{
			Log.Debug(ex, "SignalR stop failed.");
		}

		await _hubConnection.DisposeAsync();
		_hubConnection = null;
	}

	/// <inheritdoc/>
	public async ValueTask DisposeAsync()
	{
		if(_disposed)
			return;

		await _gate.WaitAsync();
		try
		{
			if(_disposed)
				return;

			await StopCoreAsync();
			_disposed = true;
		}
		finally
		{
			_gate.Release();
		}

		_gate.Dispose();
		GC.SuppressFinalize(this);
	}
}
