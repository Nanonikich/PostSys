using PostSys.Application.Commands.Clients;
using PostSys.Application.Cqrs.Core;
using PostSys.Application.Exceptions;
using PostSys.Domain.Clients;
using PostSys.Domain.ValueObjects;

namespace PostSys.Application.CommandHandlers.Clients;

/// <summary>Обработчик команды изменения номера телефона клиента.</summary>
public class ChangeClientPhoneNumberCommandHandler(IClientRepository repository)
	: ICommandHandler<ChangeClientPhoneNumberCommand, bool>
{
	/// <inheritdoc/>
	public async Task<bool> Handle(ChangeClientPhoneNumberCommand command, CancellationToken cancellationToken)
	{
		var client = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Client", command.Id.ToString());

		var phoneNumberResult = PhoneNumber.Create(command.PhoneNumber);
		if(phoneNumberResult.IsFailure) throw new ArgumentException("Invalid phone number.", nameof(phoneNumberResult));

		client.ChangePhoneNumber(phoneNumberResult.Value);
		return true;
	}
}
