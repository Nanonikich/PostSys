using System;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate.Subscriptions;

using PostSys.Domain.Postmen;
using PostSys.Domain.Postmen.Checkers;
using PostSys.ReadModels.Contracts;
using PostSys.Service.Commands.Postmen;
using PostSys.Service.Common.Cqrs.Core;
using PostSys.Service.Common.Exceptions;
using PostSys.Service.Subscriptions;

namespace PostSys.Service.CommandHandlers.Postmen;

/// <summary>Определяет обработчик команды изменения данных почтальона.</summary>
/// <param name="repository"><see cref="IPostmanRepository"/>.</param>
/// <param name="checker"><see cref="IPostmanParametersChecker"/>.</param>
/// <param name="topicEventSender"><see cref="ITopicEventSender"/>.</param>
public class ChangePostmanCommandHandler(
	IPostmanRepository repository,
	IPostmanParametersChecker checker,
	ITopicEventSender topicEventSender)
	: ICommandHandler<ChangePostmanCommand, bool>
{
	#region Handlers

	/// <inheritdoc/>
	public async Task<bool> Handle(ChangePostmanCommand command, CancellationToken cancellationToken)
	{
		var postman = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Postman", command.Id.ToString());
		try
		{
			await postman.ChangeAsync(command.PostmanPackageId, checker);
			
			await topicEventSender.SendAsync(
				nameof(Subscription.OnEntityChanged),
				new EntityChangeMessageModel(
					"postman",
					command.Id),
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