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
using PostSys.Service.Subscriptions;

namespace PostSys.Service.CommandHandlers.Packages;

/// <summary>Определяет обработчик команды изменения параметров посылки.</summary>
/// <param name="repository"><see cref="IPackageRepository"/>.</param>
/// <param name="topicEventSender"><see cref="ITopicEventSender"/>.</param>
public class ChangePackageDimensionsCommandHandler(IPackageRepository repository, ITopicEventSender topicEventSender)
	: ICommandHandler<ChangePackageDimensionsCommand, bool>
{
	#region Handlers

	/// <inheritdoc/>
	public async Task<bool> Handle(ChangePackageDimensionsCommand command, CancellationToken cancellationToken)
	{
		var package = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Package", command.Id.ToString());
		var dimensionsResult = Dimensions.Create(
			command.Dimensions.Length,
			command.Dimensions.Height,
			command.Dimensions.Weight);
		if(dimensionsResult.IsFailure) throw new ArgumentException("Invalid dimensions.", nameof(dimensionsResult));

		try
		{
			package.ChangeDimensions(dimensionsResult.Value);
			
			await topicEventSender.SendAsync(
				nameof(Subscription.OnEntityParameterChanged),
				new EntityParameterChangeMessageModel(
					"package",
					"changeDimensions",
					command.Id,
					JsonSerializer.Serialize(command.Dimensions)),
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