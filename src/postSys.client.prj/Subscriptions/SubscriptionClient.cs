using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Reactive;
using System.Threading.Tasks;

using GraphQL;
using GraphQL.Client.Abstractions.Websocket;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

using NLog;

using PostSys.Client.Data.Subscriptions;
using PostSys.ReadModels.Contracts;

namespace PostSys.Client.Subscriptions;

/// <summary>Клиент для подписок на события.</summary>
public class SubscriptionClient : ISubscriptionClient
{
	#region Statics

	private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

	#endregion

	#region Data

	private List<IDisposable> _subscriptions = [];
	private readonly string _url;
	private readonly SystemTextJsonSerializer _jsonSerializer = new();
	private readonly IClientsMessageHandler _clientsHandler;
	private readonly IPostmenMessageHandler _postmenHandler;
	private readonly IPackagesMessageHandler _packagesHandler;

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="SubscriptionClient"/>.</summary>
	/// <param name="endpoint">Адрес.</param>
	/// <param name="clientsHandler">Обработчик сообщений топика clients.</param>
	/// <param name="postmenHandler">Обработчик сообщений топика postmen.</param>
	/// <param name="packagesHandler">Обработчик сообщений топика packages.</param>
	public SubscriptionClient(
		string endpoint,
		IClientsMessageHandler clientsHandler,
		IPostmenMessageHandler postmenHandler,
		IPackagesMessageHandler packagesHandler)
	{
		_url = endpoint + "/graphql";
		_clientsHandler = clientsHandler;
		_postmenHandler = postmenHandler;
		_packagesHandler = packagesHandler;
	}

	#endregion

	#region Methods

	/// <inheritdoc/>
	public async Task StartConsuming()
	{
		//await Task.WhenAll(
		//	RequestCreatedModel(),
		//	RequestChangedModel(),
		//	RequestChangedParameterModel(),
		//	RequestDeletedModel()
		//);
	}

	/// <inheritdoc/>
	public void StopConsuming()
	{
		lock(_subscriptions)
		{
			foreach(var subscription in _subscriptions)
			{
				subscription.Dispose();
			}
			_subscriptions.Clear();
		}
	}

	private async Task<IGraphQLWebSocketClient> GetGraphQlClient()
	{
		var graphQlHttpClient = new GraphQLHttpClient(_url, _jsonSerializer);
		graphQlHttpClient.Options.WebSocketEndPoint = new Uri(_url.Replace("http", "ws"));
		graphQlHttpClient.Options.UseWebSocketForQueriesAndMutations = true;

		await graphQlHttpClient.InitializeWebsocketConnection();

		_subscriptions.Add(graphQlHttpClient.WebSocketReceiveErrors.Subscribe(e =>
		{
			if(e is WebSocketException we)
				Log.Error($"WebSocketException: {we.Message} (WebSocketError {we.WebSocketErrorCode}, ErrorCode {we.ErrorCode}, NativeErrorCode {we.NativeErrorCode}");
			else
				Log.Error($"Exception in webSocket receive stream: {e}");
		}));

		return graphQlHttpClient;
	}

	private async Task RequestCreatedModel()
	{
		var requestModel = new GraphQLRequest
		{
			Query = @"
				subscription {
					onEntityCreated {
						entity
						id
					}
				}"
		};

		var graphQlClient = await GetGraphQlClient();
		_subscriptions.Add(graphQlClient.WebSocketReceiveErrors.Subscribe(e =>
		{
			if(e is WebSocketException we)
				Log.Error($"WebSocketException: {we.Message} (WebSocketError {we.WebSocketErrorCode}, ErrorCode {we.ErrorCode}, NativeErrorCode {we.NativeErrorCode}");
			else
				Log.Error($"Exception in webSocket receive stream: {e}");
		}));

		var responseStream = graphQlClient.CreateSubscriptionStream<EntityCreationMessageModel>(requestModel);

		var observer = Observer.Create<GraphQLResponse<EntityCreationMessageModel>>(async response =>
		{
			if(response.Data != null)
			{
				var message = response.Data;
				switch(message.Entity)
				{
					case "client":
						await _clientsHandler.HandleMessageAboutCreationAsync(message);
						break;
					case "postman":
						await _postmenHandler.HandleMessageAboutCreationAsync(message);
						break;
					case "package":
						await _packagesHandler.HandleMessageAboutCreationAsync(message);
						break;
				}
			}
		});

		_subscriptions.Add(responseStream.Subscribe(observer));
	}

	private async Task RequestChangedModel()
	{
		var requestModel = new GraphQLRequest
		{
			Query = @"
				subscription {
					onEntityChanged {
						entity
						id
					}
				}"
		};

		var graphQlClient = await GetGraphQlClient();
		var responseStream = graphQlClient.CreateSubscriptionStream<EntityChangeMessageModel>(requestModel);

		var observer = Observer.Create<GraphQLResponse<EntityChangeMessageModel>>(async response =>
		{
			if(response.Data != null)
			{
				var message = response.Data;
				switch(message.Entity)
				{
					case "postman":
						await _postmenHandler.HandleMessageAboutChangeAsync(message);
						break;
					case "package":
						await _packagesHandler.HandleMessageAboutChangeAsync(message);
						break;
				}
			}
		});

		_subscriptions.Add(responseStream.Subscribe(observer));
	}

	private async Task RequestChangedParameterModel()
	{
		var requestModel = new GraphQLRequest
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

		var graphQlClient = await GetGraphQlClient();
		var responseStream = graphQlClient.CreateSubscriptionStream<EntityParameterChangeMessageModel>(requestModel);

		var observer = Observer.Create<GraphQLResponse<EntityParameterChangeMessageModel>>(response =>
		{
			if(response.Data != null)
			{
				var message = response.Data;
				switch(message.Entity)
				{
					case "client":
						_clientsHandler.HandleMessageAboutChangingParameterAsync(message);
						break;
					case "postman":
						_postmenHandler.HandleMessageAboutChangingParameterAsync(message);
						break;
					case "package":
						_packagesHandler.HandleMessageAboutChangingParameterAsync(message);
						break;
				}
			}
		});

		_subscriptions.Add(responseStream.Subscribe(observer));
	}

	private async Task RequestDeletedModel()
	{
		var requestModel = new GraphQLRequest
		{
			Query = @"
				subscription {
					onEntityDeleted {
						entity
						id
					}
				}"
		};

		var graphQlClient = await GetGraphQlClient();
		var responseStream = graphQlClient.CreateSubscriptionStream<EntityDeletionMessageModel>(requestModel);

		var observer = Observer.Create<GraphQLResponse<EntityDeletionMessageModel>>(response =>
		{
			if(response.Data != null)
			{
				var message = response.Data;
				switch(message.Entity)
				{
					case "client":
						_clientsHandler.HandleMessageAboutDeletionAsync(message);
						break;
					case "postman":
						_postmenHandler.HandleMessageAboutDeletionAsync(message);
						break;
					case "package":
						_packagesHandler.HandleMessageAboutDeletionAsync(message);
						break;
				}
			}
		});

		_subscriptions.Add(responseStream.Subscribe(observer));
	}

	#endregion
}