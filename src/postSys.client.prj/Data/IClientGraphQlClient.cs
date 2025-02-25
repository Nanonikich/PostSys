using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PostSys.Client.Data;

/// <summary>Клиент GraphQL для сущности "Клиент".</summary>
public interface IClientGraphQlClient
{
	#region Methods

	/// <summary>Получает информацию о клиентах по идентификаторам.</summary>
	/// <param name="clientIds">Идентификаторы клиентов.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Асинхронная операция, возвращающая информацию о клиентах по идентификаторам.</returns>
	Task<IReadOnlyList<ReadModels.Client>> GetClientsByIdsAsync(Guid[] clientIds, CancellationToken cancellationToken = default);

	#endregion
}
