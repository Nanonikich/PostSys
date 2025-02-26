using System.Threading;
using System.Threading.Tasks;

using HotChocolate.Subscriptions;

using PostSys.Domain.Postmen;
using PostSys.ReadModels.Contracts;
using PostSys.Service.Commands.Postmen;
using PostSys.Service.Common.Cqrs.Core;
using PostSys.Service.Common.Exceptions;
using PostSys.Service.Subscriptions;

namespace PostSys.Service.CommandHandlers.Postmen;

/// <summary>Определяет обработчик команды удаления почтальона.</summary>
/// <param name="repository"><see cref="IPostmanRepository"/>.</param>
/// <param name="topicEventSender"><see cref="ITopicEventSender"/>.</param>
public class DeletePostmenCommandHandler(IPostmanRepository repository, ITopicEventSender topicEventSender)
	: ICommandHandler<DeletePostmanCommand, bool>
{
	#region Handlers

	/// <inheritdoc/>
	public async Task<bool> Handle(DeletePostmanCommand command, CancellationToken cancellationToken)
	{
		var postman = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Postman", command.Id.ToString());
		
		await topicEventSender.SendAsync(
			nameof(Subscription.OnEntityDeleted),
			new EntityDeletionMessageModel(
				"postman",
				postman.Id),
			cancellationToken);

		return postman.Delete();
	}

	#endregion
}