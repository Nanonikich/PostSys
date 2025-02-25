using System;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate;
using HotChocolate.Types;

using MediatR;

using PostSys.ReadModels.Helpers;
using PostSys.Service.Commands.Postmen;
using PostSys.Service.Common.GraphQL.Attributes;

namespace PostSys.Service.Api.Postmen;

/// <summary>Определяет мутации для почтальонов.</summary>
[MutationType]
public static class Mutations
{
	#region Methods

	/// <summary>Создает почтальона.</summary>
	/// <param name="fullname">Данные об имени почтальона.</param>
	/// <param name="packageId">Идентификатор посылки, которая принята почтальоном для доставки.</param>
	/// <param name="email">Email почтальона для доступа в систему.</param>
	/// <param name="password">Пароль почтальона для доступа в систему.</param>
	/// <param name="sender"><see cref="ISender"/>.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Идентификатор созданного почтальона.</returns>
	[UseCreateMutationConvention]
	public static Task<Guid> CreatePostmanAsync(
		Fullname fullname,
		Guid packageId,
		string email,
		string password, 
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return sender.Send(new CreatePostmanCommand(fullname, packageId, email, password), cancellationToken);
	}

	/// <summary>Обновляет данные почтальона.</summary>
	/// <param name="id">Идентификатор почтальона.</param>
	/// <param name="postmanPackageId">Идентификатор посылки для обновления.</param>
	/// <param name="sender"><see cref="ISender"/>.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Успешность выполнения запроса.</returns>
	[UseSuccessMutationConvention]
	public static async Task<bool> ChangePostmanAsync(
		Guid id,
		Guid postmanPackageId,
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return await sender.Send(new ChangePostmanCommand(id, postmanPackageId), cancellationToken);
	}

	/// <summary>Обновляет данные об имени почтальона.</summary>
	/// <param name="id">Идентификатор почтальона.</param>
	/// <param name="fullname">Данные об имени почтальона для обновления.</param>
	/// <param name="sender"><see cref="ISender"/>.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Успешность выполнения запроса.</returns>
	[UseSuccessMutationConvention]
	public static async Task<bool> ChangePostmanFullnameAsync(
		Guid id, 
		Fullname fullname,
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return await sender.Send(new ChangePostmanFullnameCommand(id, fullname), cancellationToken);
	}

	/// <summary>Обновляет Email почтальона.</summary>
	/// <param name="id">Идентификатор почтальона.</param>
	/// <param name="email">Email почтальона для обновления.</param>
	/// <param name="sender"><see cref="ISender"/>.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Успешность выполнения запроса.</returns>
	[UseSuccessMutationConvention]
	public static async Task<bool> ChangePostmanEmailAsync(
		Guid id,
		string email,
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return await sender.Send(new ChangePostmanEmailCommand(id, email), cancellationToken);
	}

	/// <summary>Обновляет пароль почтальона.</summary>
	/// <param name="id">Идентификатор почтальона.</param>
	/// <param name="password">Пароль почтальона для обновления.</param>
	/// <param name="sender"><see cref="ISender"/>.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Успешность выполнения запроса.</returns>
	[UseSuccessMutationConvention]
	public static async Task<bool> ChangePostmanPasswordAsync(
		Guid id,
		string password,
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return await sender.Send(new ChangePostmanPasswordCommand(id, password), cancellationToken);
	}

	/// <summary>Удаляет почтальона.</summary>
	/// <param name="id">Идентификатор почтальона.</param>
	/// <param name="sender"><see cref="ISender"/>.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Успешность выполнения запроса.</returns>
	[UseSuccessMutationConvention]
	public static async Task<bool> DeletePostmanAsync(
		Guid id,
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return await sender.Send(new DeletePostmanCommand(id), cancellationToken);
	}

	#endregion
}