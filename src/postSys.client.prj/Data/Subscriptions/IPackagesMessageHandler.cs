using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PostSys.ReadModels;
using PostSys.ReadModels.Contracts;
using PostSys.ReadModels.Helpers;

namespace PostSys.Client.Data.Subscriptions;

/// <summary>ќбработчик сообщений, полученных от подписок HotChocolate дл€ сущности "ѕосылка".</summary>
public interface IPackagesMessageHandler : IMessageHandler
{
    #region Events

    /// <summary>—обытие, уведомл€ющее о создании посылки.</summary>
    event EventHandler<Package>? CreatedPackageEvent;

    /// <summary>—обытие, уведомл€ющее об изменении данных посылки.</summary>
    event EventHandler<Package>? ChangedPackageEvent;

    /// <summary>—обытие, уведомл€ющее об удалении посылки.</summary>
    event EventHandler<Guid>? DeletedPackageEvent;

    /// <summary>—обытие, уведомл€ющее об изменении адреса посылки.</summary>
    event EventHandler<KeyValuePair<Guid, Address>>? ChangedPackageAddressEvent;

    /// <summary>—обытие, уведомл€ющее об изменении параметров посылки.</summary>
    event EventHandler<KeyValuePair<Guid, Dimensions>>? ChangedPackageDimensionsEvent;

    /// <summary>—обытие, уведомл€ющее об изменении статуса посылки.</summary>
    event EventHandler<KeyValuePair<Guid, PackageStatus>>? ChangedPackageStatusEvent;

    #endregion

    /// <summary>ќбработчик сообщени€ об изменении сущности, полученного по подписке.</summary>
    /// <param name="message">—ообщение об изменении сущности, полученное по подписке.</param>
    /// <returns>јсинхронна€ операци€.</returns>
    Task HandleMessageAboutChangeAsync(EntityChangeMessageModel message);
}