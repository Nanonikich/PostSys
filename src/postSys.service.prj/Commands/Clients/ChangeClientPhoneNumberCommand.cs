using System;

using PostSys.Service.Common.Cqrs.Core;

namespace PostSys.Service.Commands.Clients;

/// <summary>Определяет команду изменения номера телефона клиента.</summary>
/// <param name="Id">Идентификатор клиента.</param>
/// <param name="PhoneNumber">Номер телефона клиента для обновления.</param>
public record ChangeClientPhoneNumberCommand(Guid Id, string PhoneNumber) : ICommand<bool>;