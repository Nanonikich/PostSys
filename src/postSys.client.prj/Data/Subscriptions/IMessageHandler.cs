using System.Threading.Tasks;

using PostSys.ReadModels.Contracts;

namespace PostSys.Client.Data.Subscriptions;

/// <summary>Обработчик сообщений, полученных по подпискам HotChocolate.</summary>
public interface IMessageHandler
{
	#region Handlers

	/// <summary>Обработчик сообщения о создании сущности, полученного по подписке.</summary>
	/// <param name="message">Сообщение о создании сущности, полученное по подписке.</param>
	/// <returns>Асинхронная операция.</returns>
	Task HandleMessageAboutCreationAsync(EntityCreationMessageModel message);

	/// <summary>Обработчик сообщения об изменении параметра сущности, полученного по подписке.</summary>
	/// <param name="message">Сообщение об изменении параметра сущности, полученное по подписке.</param>
	/// <returns>Асинхронная операция.</returns>
	void HandleMessageAboutChangingParameterAsync(EntityParameterChangeMessageModel message);

	/// <summary>Обработчик сообщения об удалении сущности, полученного по подписке.</summary>
	/// <param name="message">Сообщение об удалении сущности, полученное по подписке.</param>
	/// <returns>Асинхронная операция.</returns>
	void HandleMessageAboutDeletionAsync(EntityDeletionMessageModel message);

	#endregion
}