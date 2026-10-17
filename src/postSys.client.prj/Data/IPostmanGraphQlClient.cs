using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PostSys.Client.Data;

/// <summary>Клиент GraphQL для сущности "Почтальон".</summary>
public interface IPostmanGraphQlClient
{
	/// <summary>Получает информацию о почтальонах по идентификаторам.</summary>
	/// <param name="idsOfPostmen">Идентификаторы почтальонов.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Асинхронная операция, возвращающая информацию о почтальонах по идентификаторам.</returns>
	Task<IReadOnlyList<ReadModels.Postman>> GetPostmenByIdsAsync(Guid[] idsOfPostmen, CancellationToken cancellationToken = default);

	/// <summary>Авторизует почтальона и возвращает JWT.</summary>
	Task<Contracts.AuthPayload> AuthorizeAsync(string email, string password, CancellationToken cancellationToken = default);

	/// <summary>Присваивает посылку почтальону.</summary>
	/// <param name="postmanId">Идентификатор почтальона.</param>
	/// <param name="packageId">Идентификатор посылки.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Асинхронная операция, возвращающая успешность выполнения запроса.</returns>
	Task<bool> ChangePostmanPackageAsync(Guid postmanId, Guid packageId, CancellationToken cancellationToken = default);
}