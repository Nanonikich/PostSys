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

namespace PostSys.Service.CommandHandlers.Postmen;

/// <summary>Представляет обработчик команды создания почтальона.</summary>
/// <param name="repository"><see cref="IPostmanRepository"/>.</param>
/// <param name="checker"><see cref="IPostmanParametersChecker"/>.</param>
/// <param name="topicEventSender"><see cref="ITopicEventSender"/>.</param>
public class CreatePostmanCommandHandler(
	IPostmanRepository repository,
	IPostmanParametersChecker checker,
	ITopicEventSender topicEventSender)
	: ICommandHandler<CreatePostmanCommand, Guid>
{
	#region Handlers

	/// <inheritdoc/>
	public async Task<Guid> Handle(CreatePostmanCommand command, CancellationToken cancellationToken)
	{
		var fullnameResult = Fullname.Create(
			command.Fullname.Surname,
			command.Fullname.Name,
			command.Fullname.Patronymic);
		if(fullnameResult.IsFailure) throw new ArgumentException("Invalid fullname.", nameof(fullnameResult));

		var emailResult = Email.Create(command.Email);
		if(emailResult.IsFailure) throw new ArgumentException("Invalid email.", nameof(emailResult));

		var passwordResult = Password.Create(command.Password);
		if(passwordResult.IsFailure) throw new ArgumentException("Invalid password.", nameof(passwordResult));

		try
		{
			var postman = await Postman.CreateAsync(
				fullnameResult.Value,
				command.PackageId,
				emailResult.Value,
				passwordResult.Value, checker);
			await repository.AddAsync(postman, cancellationToken);
			
			await topicEventSender.SendAsync(
				"OnEntityCreated",
				new EntityCreationMessageModel(
					"postman",
					postman.Id),
				cancellationToken);

			return postman.Id;
		}
		catch(Exception)
		{
			throw;
		}
	}

	#endregion
}