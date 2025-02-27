using System;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;

using MediatR;

using PostSys.ReadModels.Contracts;
using PostSys.ReadModels.Helpers;
using PostSys.Service.Commands.Packages;
using PostSys.Service.Common.GraphQL.Attributes;

namespace PostSys.Service.Api.Packages;

/// <summary>Определяет мутации для посылок.</summary>
[MutationType]
public static class Mutations
{
	#region Methods

	/// <summary>Создает посылку.</summary>
	/// <param name="dimensions">Параметры посылки.</param>
	/// <param name="address">Адрес.</param>
	/// <param name="status">Статус.</param>
	/// <param name="clientId">Идентификатор клиента.</param>
	/// <param name="postmanId">Идентификатор почтальона.</param>
	/// <param name="sender"><see cref="ISender"/>.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Идентификатор созданной посылки.</returns>
	[Authorize(Roles = ["admin"])]
	[UseCreateMutationConvention]
	public static Task<Guid> CreatePackageAsync(
		Dimensions dimensions,
		Address address,
		PackageStatus status,
		Guid clientId,
		Guid postmanId,
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return sender.Send(new CreatePackageCommand(dimensions, address, status, clientId, postmanId), cancellationToken);
	}

	/// <summary>Обновляет данные о посылке.</summary>
	/// <param name="id">Идентификатор посылки.</param>
	/// <param name="clientId">Идентификатор клиента для обновления.</param>
	/// <param name="postmanId">Идентификатор почтальона для обновления.</param>
	/// <param name="sender"><see cref="ISender"/>.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Успешность выполнения запроса.</returns>
	[Authorize(Roles = ["admin"])]
	[UseSuccessMutationConvention]
	public static async Task<bool> ChangePackageAsync(
		Guid id,
		Guid clientId,
		Guid postmanId,
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return await sender.Send(new ChangePackageCommand(id, clientId, postmanId), cancellationToken);
	}

	/// <summary>Обновляет параметры посылки.</summary>
	/// <param name="id">Идентификатор посылки.</param>
	/// <param name="dimensions">Параметры посылки для обновления.</param>
	/// <param name="sender"><see cref="ISender"/>.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Успешность выполнения запроса.</returns>
	[Authorize(Roles = ["admin"])]
	[UseSuccessMutationConvention]
	public static async Task<bool> ChangePackageDimensionsAsync(
		Guid id,
		Dimensions dimensions,
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return await sender.Send(new ChangePackageDimensionsCommand(id, dimensions), cancellationToken);
	}

	/// <summary>Обновляет местоположение посылки.</summary>
	/// <param name="id">Идентификатор посылки.</param>
	/// <param name="address">Местоположение посылки для обновления.</param>
	/// <param name="sender"><see cref="ISender"/>.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Успешность выполнения запроса.</returns>
	[Authorize(Roles = ["admin"])]
	[UseSuccessMutationConvention]
	public static async Task<bool> ChangePackageAddressAsync(
		Guid id,
		Address address,
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return await sender.Send(new ChangePackageAddressCommand(id, address), cancellationToken);
	}

	/// <summary>Обновляет статус посылки.</summary>
	/// <param name="id">Идентификатор посылки.</param>
	/// <param name="status">Статус посылки для обновления.</param>
	/// <param name="sender"><see cref="ISender"/>.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Успешность выполнения запроса.</returns>
	[Authorize(Roles = ["admin"])]
	[UseSuccessMutationConvention]
	public static async Task<bool> ChangePackageStatusAsync(
		Guid id,
		PackageStatus status,
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return await sender.Send(new ChangePackageStatusCommand(id, status), cancellationToken);
	}

	/// <summary>Удаляет посылку.</summary>
	/// <param name="id">Идентификатор посылки.</param>
	/// <param name="sender"><see cref="ISender"/>.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Успешность выполнения запроса.</returns>
	[Authorize(Roles = ["admin"])]
	[UseSuccessMutationConvention]
	public static async Task<bool> DeletePackageAsync(
		Guid id,
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return await sender.Send(new DeletePackageCommand(id), cancellationToken);
	}

	#endregion
}