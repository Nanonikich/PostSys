using PostSys.Application.Cqrs.Core;

namespace PostSys.Application.Commands.Postmen;

/// <summary>Представляет команду удаления почтальона.</summary>
/// <param name="Id">Идентификатор почтальона.</param>
public record DeletePostmanCommand(Guid Id) : ICommand<bool>;