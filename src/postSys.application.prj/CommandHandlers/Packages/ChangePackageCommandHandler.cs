using PostSys.Application.Commands.Packages;
using PostSys.Application.Cqrs.Core;
using PostSys.Application.Exceptions;
using PostSys.Domain.Packages;
using PostSys.Domain.Packages.Checkers;

namespace PostSys.Application.CommandHandlers.Packages;

/// <summary>Обработчик команды изменения данных о посылке.</summary>
public class ChangePackageCommandHandler(
	IPackageRepository repository,
	IPackageParametersChecker checker)
	: ICommandHandler<ChangePackageCommand, bool>
{
	/// <inheritdoc/>
	public async Task<bool> Handle(ChangePackageCommand command, CancellationToken cancellationToken)
	{
		var package = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Package", command.Id.ToString());

		await package.ChangeAsync(command.ClientId, command.PostmanId, checker);
		return true;
	}
}
