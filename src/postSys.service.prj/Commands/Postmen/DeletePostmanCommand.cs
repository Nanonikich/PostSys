using System;

using PostSys.Service.Common.Cqrs.Core;

namespace PostSys.Service.Commands.Postmen;

/// <summary>Представляет команду удаления почтальона.</summary>
/// <param name="Id">Идентификатор почтальона.</param>
public record DeletePostmanCommand(Guid Id) : ICommand<bool>;