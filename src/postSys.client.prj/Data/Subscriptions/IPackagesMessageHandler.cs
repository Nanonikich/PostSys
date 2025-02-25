using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PostSys.ReadModels;
using PostSys.ReadModels.Contracts;
using PostSys.ReadModels.Helpers;

namespace PostSys.Client.Data.Subscriptions;

/// <summary>Обработчик сообщений, полученных от подписок HotChocolate для сущности "Посылка".</summary>
public interface IPackagesMessageHandler : IMessageHandler
{
	#region Events

	/// <summary>Событие, уведомляющее о создании посылки.</summary>
	event EventHandler<Package> CreatedPackageEvent;

	/// <summary>Событие, уведомляющее об изменении данных посылки.</summary>
	event EventHandler<Package> ChangedPackageEvent;

	/// <summary>Событие, уведомляющее об удалении посылки.</summary>
	event EventHandler<Guid> DeletedPackageEvent;

	/// <summary>Событие, уведомляющее об изменении адреса посылки.</summary>
	event EventHandler<KeyValuePair<Guid, Address>> ChangedPackageAddressEvent;

	/// <summary>Событие, уведомляющее об изменении параметров посылки.</summary>
	event EventHandler<KeyValuePair<Guid, Dimensions>> ChangedPackageDimensionsEvent;

	/// <summary>Событие, уведомляющее об изменении статуса посылки.</summary>
	event EventHandler<KeyValuePair<Guid, PackageStatus>> ChangedPackageStatusEvent;

	#endregion

	#region Handlers

	/// <summary>Обработчик сообщения об изменении сущности, полученного по подписке.</summary>
	/// <param name="message">Сообщение об изменении сущности, полученное по подписке.</param>
	/// <returns>Асинхронная операция.</returns>
	Task HandleMessageAboutChangeAsync(EntityChangeMessageModel message);

	#endregion
}
