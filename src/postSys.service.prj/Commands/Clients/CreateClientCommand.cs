using System;

using PostSys.ReadModels.Helpers;
using PostSys.Service.Common.Cqrs.Core;

namespace PostSys.Service.Commands.Clients;

/// <summary>Представляет команду создания клиента.</summary>
/// <param name="Fullname">Данные об имени клиента.</param>
/// <param name="PhoneNumber">Номер телефона клиента.</param>
public record CreateClientCommand(Fullname Fullname, string PhoneNumber) : ICommand<Guid>;