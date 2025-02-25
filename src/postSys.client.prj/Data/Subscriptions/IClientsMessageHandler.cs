using System;
using System.Collections.Generic;

using PostSys.ReadModels.Helpers;

namespace PostSys.Client.Data.Subscriptions;

/// <summary>Обработчик сообщений, полученных от подписок HotChocolate для сущности "Клиент".</summary>
public interface IClientsMessageHandler : IMessageHandler
{
	#region Events

	/// <summary>Событие, уведомляющее о создании клиента.</summary>
	event EventHandler<ReadModels.Client> CreatedClientEvent;

	/// <summary>Событие, уведомляющее об удалении клиента.</summary>
	event EventHandler<Guid> DeletedClientEvent;

	/// <summary>Событие, уведомляющее об изменении полного имени клиента.</summary>
	event EventHandler<KeyValuePair<Guid, Fullname>> ChangedClientFullnameEvent;

	/// <summary>Событие, уведомляющее об изменении номера телефона клиента.</summary>
	event EventHandler<KeyValuePair<Guid, string>> ChangedClientPhoneNumberEvent;

	#endregion
}
