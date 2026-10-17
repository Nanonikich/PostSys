using PostSys.Application.Contracts;
using PostSys.Application.Cqrs.Core;

namespace PostSys.Application.Commands.Clients;

/// <summary>Представляет команду создания клиента.</summary>
/// <param name="Fullname">Данные об имени клиента.</param>
/// <param name="PhoneNumber">Номер телефона клиента.</param>
public record CreateClientCommand(FullnameDto Fullname, string PhoneNumber) : ICommand<Guid>;