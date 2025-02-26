using System.Threading.Tasks;

namespace PostSys.Client.Data.Subscriptions;

/// <summary>Клиент для подписок на события.</summary>
public interface ISubscriptionClient
{
	#region Methods

	/// <summary>Подписывается на события HotChocolate.</summary>
	Task StartConsuming();

	/// <summary>Отписывается от событий HotChocolate.</summary>
	void StopConsuming();

	#endregion
}
