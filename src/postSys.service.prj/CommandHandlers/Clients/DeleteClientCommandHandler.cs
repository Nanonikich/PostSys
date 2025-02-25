using System.Threading;
using System.Threading.Tasks;

using HotChocolate.Subscriptions;

using PostSys.Domain.Clients;
using PostSys.ReadModels.Contracts;
using PostSys.Service.Commands.Clients;
using PostSys.Service.Common.Cqrs.Core;
using PostSys.Service.Common.Exceptions;

namespace PostSys.Service.CommandHandlers.Clients;

/// <summary>Определяет обработчик команды удаления клиента.</summary>
/// <param name="repository"><see cref="IClientRepository"/>.</param>
/// <param name="topicEventSender"><see cref="ITopicEventSender"/>.</param>
public class DeleteClientCommandHandler(IClientRepository repository, ITopicEventSender topicEventSender)
	: ICommandHandler<DeleteClientCommand, bool>
{
	#region Handlers

	/// <inheritdoc/>
	public async Task<bool> Handle(DeleteClientCommand command, CancellationToken cancellationToken)
	{
		var client = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Client", command.Id.ToString());
		
		await topicEventSender.SendAsync(
			"OnEntityDeleted",
			new EntityDeletionMessageModel(
				"client",
				command.Id),
			cancellationToken);

		return client.Delete();
	}

	#endregion
}