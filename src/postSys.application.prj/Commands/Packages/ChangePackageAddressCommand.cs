using PostSys.Application.Contracts;
using PostSys.Application.Cqrs.Core;

namespace PostSys.Application.Commands.Packages;

/// <summary>Представляет команду изменения местоположения посылки.</summary>
/// <param name="Id">Идентификатор посылки.</param>
/// <param name="Address">Местоположение посылки для обновления.</param>
public record ChangePackageAddressCommand(Guid Id, AddressDto Address) : ICommand<bool>;