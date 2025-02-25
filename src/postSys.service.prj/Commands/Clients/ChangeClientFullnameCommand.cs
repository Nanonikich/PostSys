using System;

using PostSys.ReadModels.Helpers;
using PostSys.Service.Common.Cqrs.Core;

namespace PostSys.Service.Commands.Clients;

/// <summary>Определяет команду изменения данных об имени клиента.</summary>
/// <param name="Id">Идентификатор клиента.</param>
/// <param name="Fullname">Данные об имени клиента для обновления.</param>
public record ChangeClientFullnameCommand(Guid Id, Fullname Fullname) : ICommand<bool>;