using System;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate.Subscriptions;

using PostSys.Domain.Clients;
using PostSys.Domain.ValueObjects;
using PostSys.ReadModels.Contracts;
using PostSys.Service.Commands.Clients;
using PostSys.Service.Common.Cqrs.Core;

namespace PostSys.Service.CommandHandlers.Clients;

/// <summary>Представляет обработчик команды создания клиента.</summary>
/// <param name="repository"><see cref="IClientRepository"/>.</param>
/// <param name="topicEventSender"><see cref="ITopicEventSender"/>.</param>
public class CreateClientCommandHandler(IClientRepository repository, ITopicEventSender topicEventSender)
	: ICommandHandler<CreateClientCommand, Guid>
{
	#region Handlers

	/// <inheritdoc/>
	public async Task<Guid> Handle(CreateClientCommand command, CancellationToken cancellationToken)
	{
		var fullnameResult = Fullname.Create(command.Fullname.Surname, command.Fullname.Name, command.Fullname.Patronymic);
		if(fullnameResult.IsFailure) throw new ArgumentException("Invalid fullname.", nameof(fullnameResult));

		var phoneNumberResult = PhoneNumber.Create(command.PhoneNumber);
		if(phoneNumberResult.IsFailure) throw new ArgumentException("Invalid phone number.", nameof(phoneNumberResult));

		try
		{
			var client = Client.Create(fullnameResult.Value, phoneNumberResult.Value);
			await repository.AddAsync(client);
			
			await topicEventSender.SendAsync(
				"OnEntityCreated",
				new EntityCreationMessageModel(
					"client",
					client.Id),
				cancellationToken);

			return client.Id;
		}
		catch(Exception)
		{
			throw;
		}
	}

	#endregion
}