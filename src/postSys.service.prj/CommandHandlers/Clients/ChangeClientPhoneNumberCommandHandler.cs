using System;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate.Subscriptions;

using PostSys.Domain.Clients;
using PostSys.Domain.ValueObjects;
using PostSys.ReadModels.Contracts;
using PostSys.Service.Commands.Clients;
using PostSys.Service.Common.Cqrs.Core;
using PostSys.Service.Common.Exceptions;

using ArgumentException = System.ArgumentException;

namespace PostSys.Service.CommandHandlers.Clients;

/// <summary>Определяет обработчик команды изменения номера телефона клиента.</summary>
/// <param name="repository"><see cref="IClientRepository"/>.</param>
/// <param name="topicEventSender"><see cref="ITopicEventSender"/>.</param>
public class ChangeClientPhoneNumberCommandHandler(IClientRepository repository, ITopicEventSender topicEventSender)
	: ICommandHandler<ChangeClientPhoneNumberCommand, bool>
{
	#region Handlers

	/// <inheritdoc/>
	public async Task<bool> Handle(ChangeClientPhoneNumberCommand command, CancellationToken cancellationToken)
	{
		var client = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Client", command.Id.ToString());
		var phoneNumberResult = PhoneNumber.Create(command.PhoneNumber);
		if(phoneNumberResult.IsFailure) throw new ArgumentException("Invalid phone number.", nameof(phoneNumberResult));

		try
		{
			client.ChangePhoneNumber(phoneNumberResult.Value);
			
			await topicEventSender.SendAsync(
				"OnEntityParameterChanged",
				new EntityParameterChangeMessageModel(
					"client",
					"changePhoneNumber",
					command.Id,
					command.PhoneNumber),
				cancellationToken);

			return true;
		}
		catch(Exception)
		{
			throw;
		}
	}

	#endregion
}