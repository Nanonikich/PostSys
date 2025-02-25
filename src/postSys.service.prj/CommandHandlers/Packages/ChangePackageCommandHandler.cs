using System;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate.Subscriptions;

using PostSys.Domain.Packages;
using PostSys.Domain.Packages.Checkers;
using PostSys.ReadModels.Contracts;
using PostSys.Service.Commands.Packages;
using PostSys.Service.Common.Cqrs.Core;
using PostSys.Service.Common.Exceptions;

namespace PostSys.Service.CommandHandlers.Packages;

/// <summary>Определяет обработчик команды изменения данных о посылке.</summary>
/// <param name="repository"><see cref="IPackageRepository"/>.</param>
/// <param name="checker"><see cref="IPackageParametersChecker"/>.</param>
/// <param name="topicEventSender"><see cref="ITopicEventSender"/>.</param>
public class ChangePackageCommandHandler(
	IPackageRepository repository,
	IPackageParametersChecker checker,
	ITopicEventSender topicEventSender)
	: ICommandHandler<ChangePackageCommand, bool>
{
	#region Handlers

	/// <inheritdoc/>
	public async Task<bool> Handle(ChangePackageCommand command, CancellationToken cancellationToken)
	{
		var package = await repository.FindAsync(command.Id, cancellationToken);
		if(package == null) throw new EntityNotFoundException("Package", command.Id.ToString());

		try
		{
			await package.ChangeAsync(command.ClientId, command.PostmanId, checker);
			
			await topicEventSender.SendAsync(
				"OnEntityChanged",
				new EntityChangeMessageModel(
					"package",
					command.Id),
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