using System;
using System.Collections.Generic;
using PostSys.ReadModels.Helpers;

namespace PostSys.Client.Data.Subscriptions;

/// <summary>ќбработчик сообщений, полученных от подписок HotChocolate дл€ сущности " лиент".</summary>
public interface IClientsMessageHandler : IMessageHandler
{
    /// <summary>—обытие, уведомл€ющее о создании клиента.</summary>
    event EventHandler<ReadModels.Client>? CreatedClientEvent;

    /// <summary>—обытие, уведомл€ющее об удалении клиента.</summary>
    event EventHandler<Guid>? DeletedClientEvent;

    /// <summary>—обытие, уведомл€ющее об изменении полного имени клиента.</summary>
    event EventHandler<KeyValuePair<Guid, Fullname>>? ChangedClientFullnameEvent;

    /// <summary>—обытие, уведомл€ющее об изменении номера телефона клиента.</summary>
    event EventHandler<KeyValuePair<Guid, string>>? ChangedClientPhoneNumberEvent;
}