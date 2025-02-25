using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate.Subscriptions;

using PostSys.Domain.Packages;
using PostSys.Domain.ValueObjects;
using PostSys.ReadModels.Contracts;
using PostSys.Service.Commands.Packages;
using PostSys.Service.Common.Cqrs.Core;
using PostSys.Service.Common.Exceptions;

namespace PostSys.Service.CommandHandlers.Packages;

/// <summary>Определяет обработчик команды изменения данных о местоположении посылки.</summary>
/// <param name="repository"><see cref="IPackageRepository"/>.</param>
/// <param name="topicEventSender"><see cref="ITopicEventSender"/>.</param>
public class ChangePackageAddressCommandHandler(IPackageRepository repository, ITopicEventSender topicEventSender)
	: ICommandHandler<ChangePackageAddressCommand, bool>
{
	#region Handlers

	/// <inheritdoc/>
	public async Task<bool> Handle(ChangePackageAddressCommand command, CancellationToken cancellationToken)
	{
		var package = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Package", command.Id.ToString());
		var addressResult = Address.Create(command.Address.Longitude, command.Address.Latitude);
		if(addressResult.IsFailure) throw new ArgumentException("Invalid address.", nameof(addressResult));

		try
		{
			package.ChangeAddress(addressResult.Value);
			
			await topicEventSender.SendAsync(
				"OnEntityParameterChanged",
				new EntityParameterChangeMessageModel(
					"package",
					"changeAddress",
					command.Id,
					JsonSerializer.Serialize(command.Address)),
				cancellationToken);

			return true;
		}
		catch(Exception)
		{
			throw;
		}
	}

	#endregion
}