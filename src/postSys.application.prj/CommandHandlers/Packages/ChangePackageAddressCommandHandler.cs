using PostSys.Application.Commands.Packages;
using PostSys.Application.Cqrs.Core;
using PostSys.Application.Exceptions;
using PostSys.Domain.Packages;
using PostSys.Domain.ValueObjects;

namespace PostSys.Application.CommandHandlers.Packages;

/// <summary>Обработчик команды изменения местоположения посылки.</summary>
public class ChangePackageAddressCommandHandler(IPackageRepository repository)
	: ICommandHandler<ChangePackageAddressCommand, bool>
{
	/// <inheritdoc/>
	public async Task<bool> Handle(ChangePackageAddressCommand command, CancellationToken cancellationToken)
	{
		var package = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Package", command.Id.ToString());

		var addressResult = Address.Create(command.Address.Longitude, command.Address.Latitude);
		if(addressResult.IsFailure) throw new ArgumentException("Invalid address.", nameof(addressResult));

		package.ChangeAddress(addressResult.Value);
		return true;
	}
}
