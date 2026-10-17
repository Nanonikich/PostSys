using PostSys.Application.Commands.Clients;
using PostSys.Application.Cqrs.Core;
using PostSys.Application.Exceptions;
using PostSys.Domain.Clients;
using PostSys.Domain.ValueObjects;

namespace PostSys.Application.CommandHandlers.Clients;

/// <summary>Обработчик команды изменения данных об имени клиента.</summary>
public class ChangeClientFullnameCommandHandler(IClientRepository repository)
    : ICommandHandler<ChangeClientFullnameCommand, bool>
{
    /// <inheritdoc/>
    public async Task<bool> Handle(ChangeClientFullnameCommand command, CancellationToken cancellationToken)
    {
        var client = await repository.FindAsync(command.Id, cancellationToken)
            ?? throw new EntityNotFoundException("Client", command.Id.ToString());

        var fullnameResult = Fullname.Create(
            command.Fullname.Surname,
            command.Fullname.Name,
            command.Fullname.Patronymic);
        if(fullnameResult.IsFailure) throw new ArgumentException("Invalid fullname.", nameof(fullnameResult));

        client.ChangeFullname(fullnameResult.Value);
        return true;
    }
}