using PostSys.Application.Commands.Packages;
using PostSys.Application.Cqrs.Core;
using PostSys.Application.Exceptions;
using PostSys.Domain.Packages;

namespace PostSys.Application.CommandHandlers.Packages;

/// <summary>Обработчик команды удаления посылки.</summary>
public class DeletePackageCommandHandler(IPackageRepository repository)
	: ICommandHandler<DeletePackageCommand, bool>
{
	/// <inheritdoc/>
	public async Task<bool> Handle(DeletePackageCommand command, CancellationToken cancellationToken)
	{
		var package = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Package", command.Id.ToString());

		return package.Delete();
	}
}
