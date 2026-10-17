using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PostSys.ReadModels;
using PostSys.ReadModels.Contracts;
using PostSys.ReadModels.Helpers;

namespace PostSys.Client.Data.Subscriptions;

/// <summary>ќбработчик сообщений, полученных от подписок HotChocolate дл€ сущности "ѕочтальон".</summary>
public interface IPostmenMessageHandler : IMessageHandler
{
    #region Events

    /// <summary>—обытие, уведомл€ющее о создании почтальона.</summary>
    event EventHandler<Postman>? CreatedPostmanEvent;

    /// <summary>—обытие, уведомл€ющее об изменении данных почтальона.</summary>
    event EventHandler<Postman>? ChangedPostmanEvent;

    /// <summary>—обытие, уведомл€ющее об удалении почтальона.</summary>
    event EventHandler<Guid>? DeletedPostmanEvent;

    /// <summary>—обытие, уведомл€ющее об изменении email почтальона.</summary>
    event EventHandler<KeyValuePair<Guid, string>>? ChangedEmailPostmanEvent;

    /// <summary>—обытие, уведомл€ющее об изменении полного имени почтальона.</summary>
    event EventHandler<KeyValuePair<Guid, Fullname>>? ChangedFullnamePostmanEvent;

    /// <summary>—обытие, уведомл€ющее об изменении парол€ почтальона.</summary>
    event EventHandler<KeyValuePair<Guid, string>>? ChangedPasswordPostmanEvent;

    #endregion

    /// <summary>ќбработчик сообщени€ об изменении сущности, полученного по подписке.</summary>
    /// <param name="message">—ообщение об изменении сущности, полученное по подписке.</param>
    /// <returns>јсинхронна€ операци€.</returns>
    Task HandleMessageAboutChangeAsync(EntityChangeMessageModel message);
}