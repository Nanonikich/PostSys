using System;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;

using MediatR;

using PostSys.ReadModels.Helpers;
using PostSys.Service.Commands.Clients;
using PostSys.Service.Common.GraphQL.Attributes;

namespace PostSys.Service.Api.Clients;

/// <summary>Определяет мутации для клиентов.</summary>
[MutationType]
public static class Mutations
{
	#region Methods

	/// <summary>Создает клиента.</summary>
	/// <param name="fullname">Данные об имени клиента.</param>
	/// <param name="phoneNumber">Номер телефона клиента.</param>
	/// <param name="sender"><see cref="ISender"/>.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Идентификатор созданного клиента.</returns>
	[Authorize(Roles = [ "admin" ])]
	[UseCreateMutationConvention]
	public static Task<Guid> CreateClientAsync(
		Fullname fullname,
		string phoneNumber,
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return sender.Send(new CreateClientCommand(fullname, phoneNumber), cancellationToken);
	}

	/// <summary>Обновляет данные об имени клиента.</summary>
	/// <param name="id">Идентификатор клиента.</param>
	/// <param name="fullname">Данные об имени клиента для обновления.</param>
	/// <param name="sender"><see cref="ISender"/>.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Успешность выполнения запроса.</returns>
	[Authorize(Roles = ["admin"])]
	[UseSuccessMutationConvention]
	public static async Task<bool> ChangeClientFullnameAsync(
		Guid id,
		Fullname fullname,
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return await sender.Send(new ChangeClientFullnameCommand(id, fullname), cancellationToken);
	}

	/// <summary>Обновляет номер телефона клиента.</summary>
	/// <param name="id">Идентификатор клиента.</param>
	/// <param name="phoneNumber">Номер телефона клиента для обновления.</param>
	/// <param name="sender"><see cref="ISender"/>.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Успешность выполнения запроса.</returns>
	[Authorize(Roles = ["admin"])]
	[UseSuccessMutationConvention]
	public static async Task<bool> ChangeClientPhoneNumberAsync(
		Guid id,
		string phoneNumber,
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return await sender.Send(new ChangeClientPhoneNumberCommand(id, phoneNumber), cancellationToken);
	}

	/// <summary>Удаляет клиента.</summary>
	/// <param name="id">Идентификатор клиента.</param>
	/// <param name="sender"><see cref="ISender"/>.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Успешность выполнения запроса.</returns>
	[Authorize(Roles = ["admin"])]
	[UseSuccessMutationConvention]
	public static async Task<bool> DeleteClientAsync(
		Guid id,
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return await sender.Send(new DeleteClientCommand(id), cancellationToken);
	}

	#endregion
}