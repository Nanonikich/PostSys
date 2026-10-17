using PostSys.Application.Commands.Packages;
using PostSys.Application.Cqrs.Core;
using PostSys.Application.Exceptions;
using PostSys.Domain.Packages;
using PostSys.Domain.ValueObjects;

namespace PostSys.Application.CommandHandlers.Packages;

/// <summary>Обработчик команды изменения параметров посылки.</summary>
public class ChangePackageDimensionsCommandHandler(IPackageRepository repository)
	: ICommandHandler<ChangePackageDimensionsCommand, bool>
{
	/// <inheritdoc/>
	public async Task<bool> Handle(ChangePackageDimensionsCommand command, CancellationToken cancellationToken)
	{
		var package = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Package", command.Id.ToString());

		var dimensionsResult = Dimensions.Create(
			command.Dimensions.Length ?? 0,
			command.Dimensions.Height ?? 0,
			command.Dimensions.Weight);
		if(dimensionsResult.IsFailure) throw new ArgumentException("Invalid dimensions.", nameof(dimensionsResult));

		package.ChangeDimensions(dimensionsResult.Value);
		return true;
	}
}
