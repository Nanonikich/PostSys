using PostSys.Application.Commands.Packages;
using PostSys.Application.Cqrs.Core;
using PostSys.Application.Exceptions;
using PostSys.Domain.Packages;
using PostSys.Domain.ValueObjects;

namespace PostSys.Application.CommandHandlers.Packages;

/// <summary>Обработчик команды изменения статуса посылки.</summary>
public class ChangePackageStatusCommandHandler(IPackageRepository repository)
    : ICommandHandler<ChangePackageStatusCommand, bool>
{
    /// <inheritdoc/>
    public async Task<bool> Handle(ChangePackageStatusCommand command, CancellationToken cancellationToken)
    {
        var package = await repository.FindAsync(command.Id, cancellationToken)
            ?? throw new EntityNotFoundException("Package", command.Id.ToString());

        var statusResult = PackageStatus.Create(command.Status);
        if(statusResult.IsFailure) throw new ArgumentException("Invalid status.", nameof(statusResult));

        package.ChangeStatus(statusResult.Value);
        return true;
    }
}