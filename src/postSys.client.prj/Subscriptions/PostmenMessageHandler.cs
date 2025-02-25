using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

using PostSys.Client.Data;
using PostSys.Client.Data.Subscriptions;
using PostSys.ReadModels;
using PostSys.ReadModels.Contracts;
using PostSys.ReadModels.Helpers;

namespace PostSys.Client.Subscriptions;

/// <summary>Обработчик сообщений, полученных от подписок HotChocolate для сущности "Почтальон".</summary>
/// <param name="postmanGraphQlClient">Клиент GraphQL для сущности "Почтальон".</param>
public class PostmenMessageHandler(IPostmanGraphQlClient postmanGraphQlClient) : IPostmenMessageHandler
{
	#region Events

	/// <inheritdoc/>
	public event EventHandler<Postman> CreatedPostmanEvent;

	/// <inheritdoc/>
	public event EventHandler<Postman> ChangedPostmanEvent;

	/// <inheritdoc/>
	public event EventHandler<Guid> DeletedPostmanEvent;

	/// <inheritdoc/>
	public event EventHandler<KeyValuePair<Guid, string>> ChangedEmailPostmanEvent;

	/// <inheritdoc/>
	public event EventHandler<KeyValuePair<Guid, Fullname>> ChangedFullnamePostmanEvent;

	/// <inheritdoc/>
	public event EventHandler<KeyValuePair<Guid, string>> ChangedPasswordPostmanEvent;

	#endregion

	#region Handlers

	/// <inheritdoc/>
	public async Task HandleMessageAboutCreationAsync(EntityCreationMessageModel message)
	{
		var createdPostman = await postmanGraphQlClient.GetPostmenByIdsAsync([message.Id]);
		CreatedPostmanEvent?.Invoke(this, createdPostman[0]);
	}

	/// <inheritdoc/>
	public async Task HandleMessageAboutChangeAsync(EntityChangeMessageModel message)
	{
		var changedPostman = await postmanGraphQlClient.GetPostmenByIdsAsync([message.Id]);
		ChangedPostmanEvent?.Invoke(this, changedPostman[0]);
	}

	/// <inheritdoc/>
	public void HandleMessageAboutChangingParameterAsync(EntityParameterChangeMessageModel message)
	{
		 if(message.Action == "changeEmail" && message.Value != null)
		{
			ChangedEmailPostmanEvent?.Invoke(this, new KeyValuePair<Guid, string>(message.Id, message.Value.ToString()!));
		}
		else if(message.Action == "changeFullname" && message.Value != null)
		{
			ChangedFullnamePostmanEvent?.Invoke(this, new KeyValuePair<Guid, Fullname>(message.Id, JsonSerializer.Deserialize<Fullname>(message.Value)!));
		}
		else if(message.Action == "changePassword" && message.Value != null)
		{
			ChangedPasswordPostmanEvent?.Invoke(this, new KeyValuePair<Guid, string>(message.Id, message.Value!));
		}
	}

	/// <inheritdoc/>
	public void HandleMessageAboutDeletionAsync(EntityDeletionMessageModel message)
	{
		DeletedPostmanEvent?.Invoke(this, message.Id);
	}

	#endregion
}