using System.Threading;
using System.Threading.Tasks;

using HotChocolate.Subscriptions;

using PostSys.Domain.Packages;
using PostSys.ReadModels.Contracts;
using PostSys.Service.Commands.Packages;
using PostSys.Service.Common.Cqrs.Core;
using PostSys.Service.Common.Exceptions;
using PostSys.Service.Subscriptions;

namespace PostSys.Service.CommandHandlers.Packages;

/// <summary>Определяет обработчик команды удаления посылки.</summary>
/// <param name="repository"><see cref="IPackageRepository"/>.</param>
/// <param name="topicEventSender"><see cref="ITopicEventSender"/>.</param>
public class DeletePackageCommandHandler(IPackageRepository repository, ITopicEventSender topicEventSender)
	: ICommandHandler<DeletePackageCommand, bool>
{
	#region Handlers

	/// <inheritdoc/>
	public async Task<bool> Handle(DeletePackageCommand command, CancellationToken cancellationToken)
	{
		var package = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Package", command.Id.ToString());
		
		await topicEventSender.SendAsync(
			nameof(Subscription.OnEntityDeleted),
			new EntityDeletionMessageModel(
				"package",
				command.Id),
			cancellationToken);

		return package.Delete();
	}

	#endregion
}