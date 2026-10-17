using PostSys.Application.Commands.Postmen;
using PostSys.Application.Cqrs.Core;
using PostSys.Application.Exceptions;
using PostSys.Domain.Postmen;
using PostSys.Domain.Postmen.Checkers;
using PostSys.Domain.ValueObjects;

namespace PostSys.Application.CommandHandlers.Postmen;

/// <summary>Обработчик команды изменения Email почтальона.</summary>
public class ChangePostmanEmailCommandHandler(
	IPostmanRepository repository,
	IPostmanParametersChecker checker)
	: ICommandHandler<ChangePostmanEmailCommand, bool>
{
	/// <inheritdoc/>
	public async Task<bool> Handle(ChangePostmanEmailCommand command, CancellationToken cancellationToken)
	{
		var postman = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Postman", command.Id.ToString());

		var emailResult = Email.Create(command.Email);
		if(emailResult.IsFailure) throw new ArgumentException("Invalid email.", nameof(emailResult));

		await postman.ChangeEmailAsync(emailResult.Value, checker);
		return true;
	}
}
