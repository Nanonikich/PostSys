using PostSys.Application.Commands.Clients;
using PostSys.Application.Cqrs.Core;
using PostSys.Domain.Clients;
using PostSys.Domain.ValueObjects;

namespace PostSys.Application.CommandHandlers.Clients;

/// <summary>Обработчик команды создания клиента.</summary>
public class CreateClientCommandHandler(IClientRepository repository)
	: ICommandHandler<CreateClientCommand, Guid>
{
	/// <inheritdoc/>
	public async Task<Guid> Handle(CreateClientCommand command, CancellationToken cancellationToken)
	{
		var fullnameResult = Fullname.Create(command.Fullname.Surname, command.Fullname.Name, command.Fullname.Patronymic);
		if(fullnameResult.IsFailure) throw new ArgumentException("Invalid fullname.", nameof(fullnameResult));

		var phoneNumberResult = PhoneNumber.Create(command.PhoneNumber);
		if(phoneNumberResult.IsFailure) throw new ArgumentException("Invalid phone number.", nameof(phoneNumberResult));

		var client = Client.Create(fullnameResult.Value, phoneNumberResult.Value);
		await repository.AddAsync(client);

		return client.Id;
	}
}
