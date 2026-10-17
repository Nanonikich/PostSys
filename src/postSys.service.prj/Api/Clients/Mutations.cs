using System;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate;
using HotChocolate.Types;

using MediatR;

using PostSys.Application.Commands.Clients;
using PostSys.Application.Contracts;
using PostSys.ReadModels.Helpers;
using PostSys.Service.Common.GraphQL.Attributes;

namespace PostSys.Service.Api.Clients;

/// <summary>Определяет мутации для клиентов.</summary>
[MutationType]
public static class Mutations
{
	#region Methods

	/// <summary>Создает клиента.</summary>
	[UseCreateMutationConvention]
	public static Task<Guid> CreateClientAsync(
		Fullname fullname,
		string phoneNumber,
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return sender.Send(
			new CreateClientCommand(ToDto(fullname), phoneNumber),
			cancellationToken);
	}

	/// <summary>Обновляет данные об имени клиента.</summary>
	[UseSuccessMutationConvention]
	public static Task<bool> ChangeClientFullnameAsync(
		Guid id,
		Fullname fullname,
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return sender.Send(new ChangeClientFullnameCommand(id, ToDto(fullname)), cancellationToken);
	}

	/// <summary>Обновляет номер телефона клиента.</summary>
	[UseSuccessMutationConvention]
	public static Task<bool> ChangeClientPhoneNumberAsync(
		Guid id,
		string phoneNumber,
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return sender.Send(new ChangeClientPhoneNumberCommand(id, phoneNumber), cancellationToken);
	}

	/// <summary>Удаляет клиента.</summary>
	[UseSuccessMutationConvention]
	public static Task<bool> DeleteClientAsync(
		Guid id,
		[Service] ISender sender,
		CancellationToken cancellationToken)
	{
		return sender.Send(new DeleteClientCommand(id), cancellationToken);
	}

	private static FullnameDto ToDto(Fullname fullname) =>
		new(fullname.Surname, fullname.Name, fullname.Patronymic);

	#endregion
}
