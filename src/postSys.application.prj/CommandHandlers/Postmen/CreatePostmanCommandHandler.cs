using PostSys.Application.Commands.Postmen;
using PostSys.Application.Cqrs.Core;
using PostSys.Domain.Abstractions;
using PostSys.Domain.Postmen;
using PostSys.Domain.Postmen.Checkers;
using PostSys.Domain.ValueObjects;

namespace PostSys.Application.CommandHandlers.Postmen;

/// <summary>Обработчик команды создания почтальона.</summary>
public class CreatePostmanCommandHandler(
    IPostmanRepository repository,
    IPostmanParametersChecker checker,
    IPasswordHasher passwordHasher)
    : ICommandHandler<CreatePostmanCommand, Guid>
{
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

        if(string.IsNullOrEmpty(command.Password) || command.Password.Length < 6)
            throw new ArgumentException("Invalid password.", nameof(command));

        var passwordResult = PasswordHash.Create(passwordHasher.Hash(command.Password));
        if(passwordResult.IsFailure) throw new ArgumentException("Invalid password.", nameof(passwordResult));

        var postman = await Postman.CreateAsync(
            fullnameResult.Value,
            command.PackageId,
            emailResult.Value,
            passwordResult.Value,
            checker);
        await repository.AddAsync(postman, cancellationToken);

        return postman.Id;
    }
}