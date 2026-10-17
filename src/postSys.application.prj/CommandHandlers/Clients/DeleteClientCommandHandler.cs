using PostSys.Application.Commands.Clients;
using PostSys.Application.Cqrs.Core;
using PostSys.Application.Exceptions;
using PostSys.Domain.Clients;

namespace PostSys.Application.CommandHandlers.Clients;

/// <summary>Обработчик команды удаления клиента.</summary>
public class DeleteClientCommandHandler(IClientRepository repository)
    : ICommandHandler<DeleteClientCommand, bool>
{
    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteClientCommand command, CancellationToken cancellationToken)
    {
        var client = await repository.FindAsync(command.Id, cancellationToken)
            ?? throw new EntityNotFoundException("Client", command.Id.ToString());

        return client.Delete();
    }
}