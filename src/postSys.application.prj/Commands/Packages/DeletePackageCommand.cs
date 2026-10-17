using PostSys.Application.Cqrs.Core;

namespace PostSys.Application.Commands.Packages;

/// <summary>Представляет команду удаления посылки.</summary>
/// <param name="Id">Идентификатор посылки.</param>
public record DeletePackageCommand(Guid Id) : ICommand<bool>;