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

/// <summary>Обработчик сообщений, полученных от подписок HotChocolate для сущности "Посылка".</summary>
/// <param name="packageGraphQlClient">Клиент GraphQL для сущности "Посылка".</param>
public class PackagesMessageHandler(IPackageGraphQlClient packageGraphQlClient) : IPackagesMessageHandler
{
	#region Events

	/// <inheritdoc/>
	public event EventHandler<Package> CreatedPackageEvent;

	/// <inheritdoc/>
	public event EventHandler<Package> ChangedPackageEvent;

	/// <inheritdoc/>
	public event EventHandler<Guid> DeletedPackageEvent;

	/// <inheritdoc/>
	public event EventHandler<KeyValuePair<Guid, Address>> ChangedPackageAddressEvent;

	/// <inheritdoc/>
	public event EventHandler<KeyValuePair<Guid, Dimensions>> ChangedPackageDimensionsEvent;

	/// <inheritdoc/>
	public event EventHandler<KeyValuePair<Guid, PackageStatus>> ChangedPackageStatusEvent;

	#endregion

	#region Handlers

	/// <inheritdoc/>
	public async Task HandleMessageAboutCreationAsync(EntityCreationMessageModel message)
	{
		var createdPackage = await packageGraphQlClient.GetPackagesByIdsAsync([message.Id]);
		CreatedPackageEvent?.Invoke(this, createdPackage[0]);
	}

	/// <inheritdoc/>
	public async Task HandleMessageAboutChangeAsync(EntityChangeMessageModel message)
	{
		var changedPackage = await packageGraphQlClient.GetPackagesByIdsAsync([message.Id]);
		ChangedPackageEvent?.Invoke(this, changedPackage[0]);
	}

	/// <inheritdoc/>
	public void HandleMessageAboutChangingParameterAsync(EntityParameterChangeMessageModel message)
	{
		if(message.Action == "changeAddress" && message.Value != null)
		{
			ChangedPackageAddressEvent?.Invoke(this, new KeyValuePair<Guid, Address>(message.Id, JsonSerializer.Deserialize<Address>(message.Value)!));
		}
		else if(message.Action == "changeDimensions" && message.Value != null)
		{
			ChangedPackageDimensionsEvent?.Invoke(this, new KeyValuePair<Guid, Dimensions>(message.Id, JsonSerializer.Deserialize<Dimensions>(message.Value)!));
		}
		else if(message.Action == "changeStatus" && message.Value != null)
		{
			ChangedPackageStatusEvent?.Invoke(this, new KeyValuePair<Guid, PackageStatus>(message.Id, JsonSerializer.Deserialize<PackageStatus>(message.Value)!));
		}
	}

	/// <inheritdoc/>
	public void HandleMessageAboutDeletionAsync(EntityDeletionMessageModel message)
	{
		DeletedPackageEvent?.Invoke(this, message.Id);
	}

	#endregion
}