using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

namespace PostSys.Service.SignalR;

/// <summary>Хаб уведомлений внешних сервисов.</summary>
public class NotificationHub : Hub
{
	#region Methods

	/// <summary>Уведомляет о состоянии сервиса.</summary>
	/// <param name="isAvailable">Доступен ли сервис.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Асинхронная операция.</returns>
	public async Task NotifyServiceStatus(bool isAvailable, CancellationToken cancellationToken = default)
	{
		await Clients.All.SendAsync("ReceiveServiceStatus", isAvailable, cancellationToken);
	}

	#endregion
}