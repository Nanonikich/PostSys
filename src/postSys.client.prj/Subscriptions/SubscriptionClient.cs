using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Reactive;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using NLog;
using PostSys.Client.Data.Subscriptions;
using PostSys.Client.GraphQl;
using PostSys.ReadModels.Contracts;

namespace PostSys.Client.Subscriptions;

/// <summary>Клиент подписок на события сущностей через поток уведомлений службы.</summary>
public sealed class SubscriptionClient : ISubscriptionClient
{
    private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

    private readonly GraphQlConnection _connection;
    private readonly IClientsMessageHandler _clientsHandler;
    private readonly IPostmenMessageHandler _postmenHandler;
    private readonly IPackagesMessageHandler _packagesHandler;
    private readonly object _sync = new();
    private readonly List<IDisposable> _subscriptions = [];
    private bool _running;
    private bool _disposed;

    /// <summary>Создаёт экземпляр класса <see cref="SubscriptionClient"/>.</summary>
    /// <param name="connection">Общее подключение к службе.</param>
    /// <param name="clientsHandler">Обработчик уведомлений о клиентах.</param>
    /// <param name="postmenHandler">Обработчик уведомлений о почтальонах.</param>
    /// <param name="packagesHandler">Обработчик уведомлений о посылках.</param>
    public SubscriptionClient(
        GraphQlConnection connection,
        IClientsMessageHandler clientsHandler,
        IPostmenMessageHandler postmenHandler,
        IPackagesMessageHandler packagesHandler)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        _clientsHandler = clientsHandler;
        _postmenHandler = postmenHandler;
        _packagesHandler = packagesHandler;
    }

    /// <inheritdoc/>
    public async Task StartConsuming()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        lock(_sync)
        {
            if(_running)
                return;
            _running = true;
        }

        try
        {
            await Task.WhenAll(
                SubscribeCreatedAsync(),
                SubscribeChangedAsync(),
                SubscribeParameterChangedAsync(),
                SubscribeDeletedAsync());
        }
        catch(Exception ex)
        {
            // Клиент продолжает работу без оперативных уведомлений (автономный режим / повтор позже).
            Log.Error(ex, "Failed to start GraphQL subscriptions.");
            StopConsuming();
        }
    }

    /// <inheritdoc/>
    public void StopConsuming()
    {
        lock(_sync)
        {
            foreach(var subscription in _subscriptions)
            {
                try
                {
                    subscription.Dispose();
                }
                catch(Exception ex)
                {
                    Log.Debug(ex, "Subscription dispose failed.");
                }
            }

            _subscriptions.Clear();
            _running = false;
        }
    }

    /// <inheritdoc/>
    public async Task RestartAsync()
    {
        StopConsuming();
        await StartConsuming();
    }

    /// <inheritdoc/>
    public ValueTask DisposeAsync()
    {
        if(_disposed)
            return ValueTask.CompletedTask;

        StopConsuming();
        _disposed = true;
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    private GraphQLHttpClient Client => _connection.Client;

    private void Track(IDisposable disposable)
    {
        lock(_sync)
            _subscriptions.Add(disposable);
    }

    private void TrackReceiveErrors()
    {
        Track(Client.WebSocketReceiveErrors.Subscribe(e =>
        {
            if(e is WebSocketException we)
            {
                Log.Error(
                    "WebSocketException: {Message} (WebSocketError {WebSocketError}, ErrorCode {ErrorCode}, NativeErrorCode {NativeErrorCode})",
                    we.Message,
                    we.WebSocketErrorCode,
                    we.ErrorCode,
                    we.NativeErrorCode);
            }
            else
            {
                Log.Error(e, "Exception in webSocket receive stream.");
            }
        }));
    }

    private Task SubscribeCreatedAsync()
    {
        var request = new GraphQLRequest
        {
            Query = @"
                subscription {
                    onEntityCreated {
                        entity
                        id
                    }
                }"
        };

        TrackReceiveErrors();
        var stream = Client.CreateSubscriptionStream<EntityCreationMessageModel>(request);
        Track(stream.Subscribe(Observer.Create<GraphQLResponse<EntityCreationMessageModel>>(async response =>
        {
            if(response.Data is null)
                return;

            switch(response.Data.Entity)
            {
                case "client":
                    await _clientsHandler.HandleMessageAboutCreationAsync(response.Data);
                    break;
                case "postman":
                    await _postmenHandler.HandleMessageAboutCreationAsync(response.Data);
                    break;
                case "package":
                    await _packagesHandler.HandleMessageAboutCreationAsync(response.Data);
                    break;
            }
        })));

        return Task.CompletedTask;
    }

    private Task SubscribeChangedAsync()
    {
        var request = new GraphQLRequest
        {
            Query = @"
                subscription {
                    onEntityChanged {
                        entity
                        id
                    }
                }"
        };

        var stream = Client.CreateSubscriptionStream<EntityChangeMessageModel>(request);
        Track(stream.Subscribe(Observer.Create<GraphQLResponse<EntityChangeMessageModel>>(async response =>
        {
            if(response.Data is null)
                return;

            switch(response.Data.Entity)
            {
                case "postman":
                    await _postmenHandler.HandleMessageAboutChangeAsync(response.Data);
                    break;
                case "package":
                    await _packagesHandler.HandleMessageAboutChangeAsync(response.Data);
                    break;
            }
        })));

        return Task.CompletedTask;
    }

    private Task SubscribeParameterChangedAsync()
    {
        var request = new GraphQLRequest
        {
            Query = @"
                subscription {
                    onEntityParameterChanged {
                        entity
                        id
                        action
                        value
                    }
                }"
        };

        var stream = Client.CreateSubscriptionStream<EntityParameterChangeMessageModel>(request);
        Track(stream.Subscribe(Observer.Create<GraphQLResponse<EntityParameterChangeMessageModel>>(response =>
        {
            if(response.Data is null)
                return;

            switch(response.Data.Entity)
            {
                case "client":
                    _clientsHandler.HandleMessageAboutChangingParameterAsync(response.Data);
                    break;
                case "postman":
                    _postmenHandler.HandleMessageAboutChangingParameterAsync(response.Data);
                    break;
                case "package":
                    _packagesHandler.HandleMessageAboutChangingParameterAsync(response.Data);
                    break;
            }
        })));

        return Task.CompletedTask;
    }

    private Task SubscribeDeletedAsync()
    {
        var request = new GraphQLRequest
        {
            Query = @"
                subscription {
                    onEntityDeleted {
                        entity
                        id
                    }
                }"
        };

        var stream = Client.CreateSubscriptionStream<EntityDeletionMessageModel>(request);
        Track(stream.Subscribe(Observer.Create<GraphQLResponse<EntityDeletionMessageModel>>(response =>
        {
            if(response.Data is null)
                return;

            switch(response.Data.Entity)
            {
                case "client":
                    _clientsHandler.HandleMessageAboutDeletionAsync(response.Data);
                    break;
                case "postman":
                    _postmenHandler.HandleMessageAboutDeletionAsync(response.Data);
                    break;
                case "package":
                    _packagesHandler.HandleMessageAboutDeletionAsync(response.Data);
                    break;
            }
        })));

        return Task.CompletedTask;
    }
}