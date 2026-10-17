using PostSys.Application.Commands.Postmen;
using PostSys.Application.Cqrs.Core;
using PostSys.Application.Exceptions;
using PostSys.Domain.Postmen;

namespace PostSys.Application.CommandHandlers.Postmen;

/// <summary>Обработчик команды удаления почтальона.</summary>
public class DeletePostmanCommandHandler(IPostmanRepository repository)
	: ICommandHandler<DeletePostmanCommand, bool>
{
	/// <inheritdoc/>
	public async Task<bool> Handle(DeletePostmanCommand command, CancellationToken cancellationToken)
	{
		var postman = await repository.FindAsync(command.Id, cancellationToken)
			?? throw new EntityNotFoundException("Postman", command.Id.ToString());

		return postman.Delete();
	}
}
