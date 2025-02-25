using System;

using PostSys.Service.Common.Cqrs.Core;

namespace PostSys.Service.Commands.Packages;

/// <summary>Представляет команду удаления посылки.</summary>
/// <param name="Id">Идентификатор посылки.</param>
public record DeletePackageCommand(Guid Id) : ICommand<bool>;