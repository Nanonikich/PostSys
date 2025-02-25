using System;

using PostSys.Service.Common.Cqrs.Core;

namespace PostSys.Service.Commands.Clients;

/// <summary>Представляет команду удаления клиента.</summary>
/// <param name="Id">Идентификатор клиента.</param>
public record DeleteClientCommand(Guid Id) : ICommand<bool>;