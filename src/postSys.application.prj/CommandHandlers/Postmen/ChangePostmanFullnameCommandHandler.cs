using PostSys.Application.Commands.Postmen;
using PostSys.Application.Cqrs.Core;
using PostSys.Application.Exceptions;
using PostSys.Domain.Postmen;
using PostSys.Domain.ValueObjects;

namespace PostSys.Application.CommandHandlers.Postmen;

/// <summary>Обработчик команды изменения данных об имени почтальона.</summary>
public class ChangePostmanFullnameCommandHandler(IPostmanRepository repository)
    : ICommandHandler<ChangePostmanFullnameCommand, bool>
{
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

        postman.ChangeFullname(fullnameResult.Value);
        return true;
    }
}