using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PostSys.ReadModels;
using PostSys.ReadModels.Contracts;
using PostSys.ReadModels.Helpers;

namespace PostSys.Client.Data.Subscriptions;

/// <summary>Обработчик сообщений, полученных от подписок HotChocolate для сущности "Почтальон".</summary>
public interface IPostmenMessageHandler : IMessageHandler
{
	#region Events

	/// <summary>Событие, уведомляющее о создании почтальона.</summary>
	event EventHandler<Postman> CreatedPostmanEvent;

	/// <summary>Событие, уведомляющее об изменении данных почтальона.</summary>
	event EventHandler<Postman> ChangedPostmanEvent;

	/// <summary>Событие, уведомляющее об удалении почтальона.</summary>
	event EventHandler<Guid> DeletedPostmanEvent;

	/// <summary>Событие, уведомляющее об изменении email почтальона.</summary>
	event EventHandler<KeyValuePair<Guid, string>> ChangedEmailPostmanEvent;

	/// <summary>Событие, уведомляющее об изменении полного имени почтальона.</summary>
	event EventHandler<KeyValuePair<Guid, Fullname>> ChangedFullnamePostmanEvent;

	/// <summary>Событие, уведомляющее об изменении пароля почтальона.</summary>
	event EventHandler<KeyValuePair<Guid, string>> ChangedPasswordPostmanEvent;

	#endregion

	#region Handlers

	/// <summary>Обработчик сообщения об изменении сущности, полученного по подписке.</summary>
	/// <param name="message">Сообщение об изменении сущности, полученное по подписке.</param>
	/// <returns>Асинхронная операция.</returns>
	Task HandleMessageAboutChangeAsync(EntityChangeMessageModel message);

	#endregion
}
