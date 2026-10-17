using PostSys.Application.Commands.Postmen;
using PostSys.Application.Cqrs.Core;
using PostSys.Application.Exceptions;
using PostSys.Domain.Postmen;
using PostSys.Domain.Postmen.Checkers;

namespace PostSys.Application.CommandHandlers.Postmen;

/// <summary>Обработчик команды изменения данных почтальона.</summary>
public class ChangePostmanCommandHandler(
    IPostmanRepository repository,
    IPostmanParametersChecker checker)
    : ICommandHandler<ChangePostmanCommand, bool>
{
    /// <inheritdoc/>
    public async Task<bool> Handle(ChangePostmanCommand command, CancellationToken cancellationToken)
    {
        var postman = await repository.FindAsync(command.Id, cancellationToken)
            ?? throw new EntityNotFoundException("Postman", command.Id.ToString());

        await postman.ChangeAsync(command.PostmanPackageId, checker);
        return true;
    }
}
