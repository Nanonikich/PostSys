using System;
using System.Threading.Tasks;

namespace PostSys.Client.Data.Subscriptions;

/// <summary>Клиент для подписок на события.</summary>
public interface ISubscriptionClient : IAsyncDisposable
{
    /// <summary>Подписывается на события HotChocolate.</summary>
    Task StartConsuming();

    /// <summary>Отписывается от событий HotChocolate.</summary>
    void StopConsuming();

    /// <summary>Перезапускает подписки (например после смены адреса сервиса).</summary>
    Task RestartAsync();
}