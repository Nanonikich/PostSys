using HotChocolate;
using HotChocolate.Types;

using PostSys.ReadModels.Contracts;

namespace PostSys.Service.Subscriptions;

/// <summary>Менеджер подписок HotChocolate.</summary>
public class Subscription
{
	/// <summary>Обработчик создания сущности.</summary>
	/// <param name="payload">Модель сообщения, отправляемого подписчикам.</param>
	/// <returns>Модель сообщения, отправляемого подписчикам.</returns>
	[Subscribe]
	public EntityCreationMessageModel OnEntityCreated([EventMessage] EntityCreationMessageModel payload) => payload;

	/// <summary>Обработчик изменения сущности.</summary>
	/// <param name="payload">Модель сообщения, отправляемого подписчикам.</param>
	/// <returns>Модель сообщения, отправляемого подписчикам.</returns>
	[Subscribe]
	public EntityChangeMessageModel OnEntityChanged([EventMessage] EntityChangeMessageModel payload) => payload;

	/// <summary>Обработчик изменения параметра сущности.</summary>
	/// <param name="payload">Модель сообщения, отправляемого подписчикам.</param>
	/// <returns>Модель сообщения, отправляемого подписчикам.</returns>
	[Subscribe]
	public EntityParameterChangeMessageModel OnEntityParameterChanged([EventMessage] EntityParameterChangeMessageModel payload) => payload;

	/// <summary>Обработчик создания сущности.</summary>
	/// <param name="payload">Модель сообщения, отправляемого подписчикам.</param>
	/// <returns>Модель сообщения, отправляемого подписчикам.</returns>
	[Subscribe]
	public EntityDeletionMessageModel OnEntityDeleted([EventMessage] EntityDeletionMessageModel payload) => payload;
}
