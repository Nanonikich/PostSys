using System;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate.Subscriptions;

using PostSys.Domain.Postmen;
using PostSys.Domain.Postmen.Checkers;
using PostSys.Domain.ValueObjects;
using PostSys.ReadModels.Contracts;
using PostSys.Service.Commands.Postmen;
using PostSys.Service.Common.Cqrs.Core;
using PostSys.Service.Common.Exceptions;
using PostSys.Service.Subscriptions;

namespace PostSys.Service.CommandHandlers.Postmen;

/// <summary>Определяет обработчик команды изменения Email почтальона.</summary>
/// <param name="repository"><see cref="IPostmanRepository"/>.</param>
/// <param name="checker"><see cref="IPostmanParametersChecker"/>.</param>
/// <param name="topicEventSender"><see cref="ITopicEventSender"/>.</param>
public class ChangePostmanEmailCommandHandler(
	IPostmanRepository repository,
	IPostmanParametersChecker checker,
	ITopicEventSender topicEventSender)
	: ICommandHandler<ChangePostmanEmailCommand, bool>
{
	#region Handlers

	/// <inheritdoc/>
	public async Task<bool> Handle(ChangePostmanEmailCommand command, CancellationToken cancellationToken)
	{
		var postman = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Postman", command.Id.ToString());
		var emailResult = Email.Create(command.Email);
		if(emailResult.IsFailure) throw new ArgumentException("Invalid email.", nameof(emailResult));

		try
		{
			await postman.ChangeEmailAsync(emailResult.Value, checker);
			
			await topicEventSender.SendAsync(
				nameof(Subscription.OnEntityParameterChanged),
				new EntityParameterChangeMessageModel(
					"postman",
					"changeEmail",
					command.Id,
					command.Email),
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