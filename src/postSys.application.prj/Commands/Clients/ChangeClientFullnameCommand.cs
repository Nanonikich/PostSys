using PostSys.Application.Contracts;
using PostSys.Application.Cqrs.Core;

namespace PostSys.Application.Commands.Clients;

/// <summary>Определяет команду изменения данных об имени клиента.</summary>
/// <param name="Id">Идентификатор клиента.</param>
/// <param name="Fullname">Данные об имени клиента для обновления.</param>
public record ChangeClientFullnameCommand(Guid Id, FullnameDto Fullname) : ICommand<bool>;
