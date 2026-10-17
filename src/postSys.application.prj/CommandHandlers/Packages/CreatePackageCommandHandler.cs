using PostSys.Application.Commands.Packages;
using PostSys.Application.Cqrs.Core;
using PostSys.Domain.Packages;
using PostSys.Domain.Packages.Checkers;
using PostSys.Domain.ValueObjects;

namespace PostSys.Application.CommandHandlers.Packages;

/// <summary>Обработчик команды создания посылки.</summary>
public class CreatePackageCommandHandler(
	IPackageRepository repository,
	IPackageParametersChecker checker)
	: ICommandHandler<CreatePackageCommand, Guid>
{
	/// <inheritdoc/>
	public async Task<Guid> Handle(CreatePackageCommand command, CancellationToken cancellationToken)
	{
		var dimensionsResult = Dimensions.Create(
			command.Dimensions.Length ?? 0,
			command.Dimensions.Height ?? 0,
			command.Dimensions.Weight);
		if(dimensionsResult.IsFailure) throw new ArgumentException("Invalid dimensions.", nameof(dimensionsResult));

		var addressResult = Address.Create(command.Address.Longitude, command.Address.Latitude);
		if(addressResult.IsFailure) throw new ArgumentException("Invalid address.", nameof(addressResult));

		var statusResult = PackageStatus.Create(command.Status);
		if(statusResult.IsFailure) throw new ArgumentException("Invalid status.", nameof(statusResult));

		var package = await Package.CreateAsync(
			dimensionsResult.Value,
			addressResult.Value,
			statusResult.Value,
			command.ClientId,
			command.PostmanId,
			checker);
		await repository.AddAsync(package);

		return package.Id;
	}
}
