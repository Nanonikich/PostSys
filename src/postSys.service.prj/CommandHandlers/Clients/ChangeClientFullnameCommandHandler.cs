using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate.Subscriptions;

using PostSys.Domain.Clients;
using PostSys.Domain.ValueObjects;
using PostSys.ReadModels.Contracts;
using PostSys.Service.Commands.Clients;
using PostSys.Service.Common.Cqrs.Core;
using PostSys.Service.Common.Exceptions;

namespace PostSys.Service.CommandHandlers.Clients;

/// <summary>Определяет обработчик команды изменения данных об имени клиента.</summary>
/// <param name="repository"><see cref="IClientRepository"/>.</param>
/// <param name="topicEventSender"><see cref="ITopicEventSender"/>.</param>
public class ChangeClientFullnameCommandHandler(IClientRepository repository, ITopicEventSender topicEventSender)
	: ICommandHandler<ChangeClientFullnameCommand, bool>
{
	#region Handlers

	/// <inheritdoc/>
	public async Task<bool> Handle(ChangeClientFullnameCommand command, CancellationToken cancellationToken)
	{
		var client = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Client", command.Id.ToString());
		var fullnameResult = Fullname.Create(
			command.Fullname.Surname,
			command.Fullname.Name,
			command.Fullname.Patronymic);
		if(fullnameResult.IsFailure) throw new ArgumentException("Invalid fullname.", nameof(fullnameResult));

		try
		{
			client.ChangeFullname(fullnameResult.Value);
			
			await topicEventSender.SendAsync(
				"OnEntityParameterChanged",
				new EntityParameterChangeMessageModel(
					"client",
					"changeFullname",
					command.Id,
					JsonSerializer.Serialize(command.Fullname)),
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