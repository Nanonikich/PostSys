using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

using PostSys.Client.Data;
using PostSys.Client.Data.Subscriptions;
using PostSys.ReadModels.Contracts;
using PostSys.ReadModels.Helpers;

namespace PostSys.Client.Subscriptions;

/// <summary>Обработчик сообщений, полученных от подписок HotChocolate для сущности "Клиент".</summary>
/// <param name="clientGraphQlClient">Клиент GraphQL для сущности "Клиент".</param>
public class ClientsMessageHandler(IClientGraphQlClient clientGraphQlClient) : IClientsMessageHandler
{
	#region Events

	/// <inheritdoc/>
	public event EventHandler<ReadModels.Client> CreatedClientEvent;

	/// <inheritdoc/>
	public event EventHandler<Guid> DeletedClientEvent;

	/// <inheritdoc/>
	public event EventHandler<KeyValuePair<Guid, Fullname>> ChangedClientFullnameEvent;

	/// <inheritdoc/>
	public event EventHandler<KeyValuePair<Guid, string>> ChangedClientPhoneNumberEvent;

	#endregion

	#region Handlers

	/// <inheritdoc/>
	public async Task HandleMessageAboutCreationAsync(EntityCreationMessageModel message)
	{
		var createdClient = await clientGraphQlClient.GetClientsByIdsAsync([message.Id]);
		CreatedClientEvent?.Invoke(this, createdClient[0]);
	}

	/// <inheritdoc/>
	public void HandleMessageAboutChangingParameterAsync(EntityParameterChangeMessageModel message)
	{
		if(message.Action == "changeFullname" && message.Value != null)
		{
			ChangedClientFullnameEvent?.Invoke(this, new KeyValuePair<Guid, Fullname>(message.Id, JsonSerializer.Deserialize<Fullname>(message.Value)!));
		}
		else if(message.Action == "changePhoneNumber" && message.Value != null)
		{
			ChangedClientPhoneNumberEvent?.Invoke(this, new KeyValuePair<Guid, string>(message.Id, message.Value));
		}
	}

	/// <inheritdoc/>
	public void HandleMessageAboutDeletionAsync(EntityDeletionMessageModel message)
	{
		DeletedClientEvent?.Invoke(this, message.Id);
	}

	#endregion
}