using System;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate.Subscriptions;

using PostSys.Domain.Postmen;
using PostSys.Domain.ValueObjects;
using PostSys.ReadModels.Contracts;
using PostSys.Service.Commands.Postmen;
using PostSys.Service.Common.Cqrs.Core;
using PostSys.Service.Common.Exceptions;
using PostSys.Service.Subscriptions;

namespace PostSys.Service.CommandHandlers.Postmen;

/// <summary>Определяет обработчик команды изменения пароля почтальона.</summary>
/// <param name="repository"><see cref="IPostmanRepository"/>.</param>
/// <param name="topicEventSender"><see cref="ITopicEventSender"/>.</param>
public class ChangePostmanPasswordCommandHandler(IPostmanRepository repository, ITopicEventSender topicEventSender)
	: ICommandHandler<ChangePostmanPasswordCommand, bool>
{
	#region Handlers

	/// <inheritdoc/>
	public async Task<bool> Handle(ChangePostmanPasswordCommand command, CancellationToken cancellationToken)
	{
		var postman = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Postman", command.Id.ToString());
		var passwordResult = Password.Create(command.Password);
		if(passwordResult.IsFailure) throw new ArgumentException("Invalid password.", nameof(passwordResult));

		try
		{
			postman.ChangePassword(passwordResult.Value);
			
			await topicEventSender.SendAsync(
				nameof(Subscription.OnEntityParameterChanged),
				new EntityParameterChangeMessageModel(
					"postman",
					"changePassword",
					command.Id,
					command.Password),
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