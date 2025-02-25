using System;

using Microsoft.AspNetCore.SignalR.Client;

using NLog;

namespace PostSys.Client.SignalR;

/// <summary>Политика бесконечного переподключения для технологии SignalR.</summary>
public class RetryPolicyLoop : IRetryPolicy
{
	#region Constants

	private const int ReconnectionWaitSeconds = 10;

	#endregion

	#region Static

	/// <summary>Протокол работы.</summary>
	private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

	#endregion

	#region Methods

	/// <inheritdoc/>
	public TimeSpan? NextRetryDelay(RetryContext retryContext)
	{
		Log.Error(retryContext.RetryReason, $"Service {nameof(RetryPolicyLoop):serviceName}: trying to reconnect to PostSys Hub.");

		return TimeSpan.FromSeconds(ReconnectionWaitSeconds);
	}

	#endregion
}
