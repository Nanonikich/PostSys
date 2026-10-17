using PostSys.Application.Commands.Postmen;
using PostSys.Application.Cqrs.Core;
using PostSys.Application.Exceptions;
using PostSys.Domain.Abstractions;
using PostSys.Domain.Postmen;
using PostSys.Domain.ValueObjects;

namespace PostSys.Application.CommandHandlers.Postmen;

/// <summary>Обработчик команды изменения пароля почтальона.</summary>
public class ChangePostmanPasswordCommandHandler(
	IPostmanRepository repository,
	IPasswordHasher passwordHasher)
	: ICommandHandler<ChangePostmanPasswordCommand, bool>
{
	/// <inheritdoc/>
	public async Task<bool> Handle(ChangePostmanPasswordCommand command, CancellationToken cancellationToken)
	{
		var postman = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Postman", command.Id.ToString());

		if(string.IsNullOrEmpty(command.Password) || command.Password.Length < 6)
			throw new ArgumentException("Invalid password.", nameof(command));

		var passwordResult = PasswordHash.Create(passwordHasher.Hash(command.Password));
		if(passwordResult.IsFailure) throw new ArgumentException("Invalid password.", nameof(passwordResult));

		postman.ChangePassword(passwordResult.Value);
		return true;
	}
}
