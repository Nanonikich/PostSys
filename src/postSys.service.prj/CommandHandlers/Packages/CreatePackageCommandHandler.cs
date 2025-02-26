using System;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate.Subscriptions;

using PostSys.Domain.Packages;
using PostSys.Domain.Packages.Checkers;
using PostSys.Domain.ValueObjects;
using PostSys.ReadModels.Contracts;
using PostSys.Service.Commands.Packages;
using PostSys.Service.Common.Converters;
using PostSys.Service.Common.Cqrs.Core;
using PostSys.Service.Subscriptions;

namespace PostSys.Service.CommandHandlers.Packages;

/// <summary>Представляет обработчик команды создания посылки.</summary>
/// <param name="repository"><see cref="IPackageRepository"/>.</param>
/// <param name="checker"><see cref="IPackageParametersChecker"/>.</param>
/// <param name="topicEventSender"><see cref="ITopicEventSender"/>.</param>
public class CreatePackageCommandHandler(
	IPackageRepository repository,
	IPackageParametersChecker checker,
	ITopicEventSender topicEventSender)
	: ICommandHandler<CreatePackageCommand, Guid>
{
	#region Handlers

	/// <inheritdoc/>
	public async Task<Guid> Handle(CreatePackageCommand command, CancellationToken cancellationToken)
	{
		var dimensionsResult = Dimensions.Create(
			command.Dimensions.Length,
			command.Dimensions.Height,
			command.Dimensions.Weight);
		if(dimensionsResult.IsFailure) throw new ArgumentException("Invalid dimensions.", nameof(dimensionsResult));

		var addressResult = Address.Create(command.Address.Longitude, command.Address.Latitude);
		if(addressResult.IsFailure) throw new ArgumentException("Invalid address.", nameof(addressResult));
		
		var statusResult = Domain.ValueObjects.PackageStatus.Create(EnumConverter<ReadModels.Contracts.PackageStatus, Domain.Contracts.PackageStatus>.Convert(command.Status));
		if(statusResult.IsFailure) throw new ArgumentException("Invalid status.", nameof(statusResult));

		try
		{
			var package = await Package.CreateAsync(
				dimensionsResult.Value,
				addressResult.Value,
				statusResult.Value,
				command.ClientId,
				command.PostmanId,
				checker);
			await repository.AddAsync(package);

			await topicEventSender.SendAsync(
				nameof(Subscription.OnEntityCreated),
				new EntityCreationMessageModel(
					"package",
					package.Id),
				cancellationToken);

			return package.Id;
		}
		catch(Exception)
		{
			throw;
		}
	}

	#endregion
}