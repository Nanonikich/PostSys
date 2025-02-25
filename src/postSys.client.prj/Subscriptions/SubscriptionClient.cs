using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;

using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

using PostSys.Client.Data.Subscriptions;
using PostSys.ReadModels.Contracts;

namespace PostSys.Client.Subscriptions;

/// <summary>Клиент для подписок на события.</summary>
public class SubscriptionClient : ISubscriptionClient
{
	#region Constants

	private const string PathTag = "/graphql";

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
		_url = endpoint + PathTag;
		_clientsHandler = clientsHandler;
		_postmenHandler = postmenHandler;
		_packagesHandler = packagesHandler;
	}

	#endregion

	#region Methods

	/// <inheritdoc/>
	public void StartConsuming()
	{
		RequestCreatedModel();
		RequestChangedModel();
		RequestChangedParameterModel();
		RequestDeletedModel();
	}

	/// <inheritdoc/>
	public void StopConsuming()
	{
		foreach(var subscription in _subscriptions)
		{
			subscription.Dispose();
		}
	}

	private void RequestCreatedModel()
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

		using var graphQlHttpClient = new GraphQLHttpClient(_url, _jsonSerializer);
		var responseStream = graphQlHttpClient.CreateSubscriptionStream<EntityCreationMessageModel>(requestModel);

		var observer = Observer.Create<GraphQLResponse<EntityCreationMessageModel>>(response =>
		{
			Task.Run(async () =>
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
		});

		_subscriptions.Add(responseStream.Subscribe(observer));
	}

	private void RequestChangedModel()
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

		using var graphQlHttpClient = new GraphQLHttpClient(_url, _jsonSerializer);
		var responseStream = graphQlHttpClient.CreateSubscriptionStream<EntityChangeMessageModel>(requestModel);

		var observer = Observer.Create<GraphQLResponse<EntityChangeMessageModel>>(response =>
		{
			Task.Run(async () =>
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
		});

		_subscriptions.Add(responseStream.Subscribe(observer));
	}

	private void RequestChangedParameterModel()
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

		using var graphQlHttpClient = new GraphQLHttpClient(_url, _jsonSerializer);
		var responseStream = graphQlHttpClient.CreateSubscriptionStream<EntityParameterChangeMessageModel>(requestModel);

		var observer = Observer.Create<GraphQLResponse<EntityParameterChangeMessageModel>>(response =>
		{
			Task.Run(() =>
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
		});

		_subscriptions.Add(responseStream.Subscribe(observer));
	}

	private void RequestDeletedModel()
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

		using var graphQlHttpClient = new GraphQLHttpClient(_url, _jsonSerializer);
		var responseStream = graphQlHttpClient.CreateSubscriptionStream<EntityDeletionMessageModel>(requestModel);

		var observer = Observer.Create<GraphQLResponse<EntityDeletionMessageModel>>(response =>
		{
			Task.Run(() =>
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
		});

		_subscriptions.Add(responseStream.Subscribe(observer));
	}

	#endregion
}