using PostSys.Application.Cqrs.Core;

namespace PostSys.Application.Commands.Clients;

/// <summary>Представляет команду удаления клиента.</summary>
/// <param name="Id">Идентификатор клиента.</param>
public record DeleteClientCommand(Guid Id) : ICommand<bool>;
