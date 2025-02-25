using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using PostSys.ReadModels;
using PostSys.ReadModels.Contracts;

namespace PostSys.Client.Data;

/// <summary>Клиент GraphQL для сущности "Посылка".</summary>
public interface IPackageGraphQlClient
{
	#region Methods

	/// <summary>Получает информацию о посылках по идентификаторам.</summary>
	/// <param name="packageIds">Идентификаторы посылок.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Асинхронная операция, возвращающая информацию о посылках по идентификаторам.</returns>
	Task<IReadOnlyList<Package>> GetPackagesByIdsAsync(Guid[] packageIds, CancellationToken cancellationToken = default);

	/// <summary>Получает информацию о посылках, назначенных на почтальона.</summary>
	/// <param name="postmanId">Идентификатор почтальона.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Асинхронная операция, возвращающая информацию о посылках, назначенных на почтальона.</returns>
	Task<IReadOnlyList<Package>> GetPackagesByPostmanIdAsync(Guid postmanId, CancellationToken cancellationToken = default);

	/// <summary>Изменяет статус посылки.</summary>
	/// <param name="packageId">Идентификатор посылки.</param>
	/// <param name="newStatus">Статус посылки для обновления.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Асинхронная операция, возвращающая успешность выполнения запроса.</returns>
	Task<bool> ChangePackageStatusAsync(Guid packageId, PackageStatus newStatus, CancellationToken cancellationToken = default);

	#endregion
}