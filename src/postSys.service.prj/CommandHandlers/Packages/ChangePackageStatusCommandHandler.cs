using System;
using System.Text.Json;
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

/// <summary>Определяет обработчик команды изменения данных о статусе посылки.</summary>
/// <param name="repository"><see cref="IPackageRepository"/>.</param>
/// <param name="topicEventSender"><see cref="ITopicEventSender"/>.</param>
public class ChangePackageStatusCommandHandler(IPackageRepository repository, ITopicEventSender topicEventSender)
	: ICommandHandler<ChangePackageStatusCommand, bool>
{
	#region Handlers

	/// <inheritdoc/>
	public async Task<bool> Handle(ChangePackageStatusCommand command, CancellationToken cancellationToken)
	{
		var package = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Package", command.Id.ToString());
		var statusResult = Domain.ValueObjects.PackageStatus.Create((Domain.Contracts.PackageStatus)command.Status);
		if(statusResult.IsFailure) throw new ArgumentException("Invalid status.", nameof(statusResult));

		try
		{
			package.ChangeStatus(statusResult.Value);
			
			await topicEventSender.SendAsync(
				nameof(Subscription.OnEntityParameterChanged),
				new EntityParameterChangeMessageModel(
					"package",
					"changeStatus",
					command.Id,
					JsonSerializer.Serialize(command.Status)),
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
