using System;
using System.Text.Json;
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

/// <summary>Определяет обработчик команды изменения данных об имени почтальона.</summary>
/// <param name="repository"><see cref="IPostmanRepository"/>.</param>
/// <param name="topicEventSender"><see cref="ITopicEventSender"/>.</param>
public class ChangePostmanFullnameCommandHandler(IPostmanRepository repository, ITopicEventSender topicEventSender)
	: ICommandHandler<ChangePostmanFullnameCommand, bool>
{
	#region Handlers

	/// <inheritdoc/>
	public async Task<bool> Handle(ChangePostmanFullnameCommand command, CancellationToken cancellationToken)
	{
		var postman = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Postman", command.Id.ToString());
		var fullnameResult = Fullname.Create(
			command.Fullname.Surname,
			command.Fullname.Name,
			command.Fullname.Patronymic);
		if(fullnameResult.IsFailure) throw new ArgumentException("Invalid fullname.", nameof(fullnameResult));

		try
		{
			postman.ChangeFullname(fullnameResult.Value);
			
			await topicEventSender.SendAsync(
				nameof(Subscription.OnEntityParameterChanged),
				new EntityParameterChangeMessageModel(
					"postman",
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