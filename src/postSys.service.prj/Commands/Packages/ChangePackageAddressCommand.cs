using System;

using PostSys.ReadModels.Helpers;
using PostSys.Service.Common.Cqrs.Core;

namespace PostSys.Service.Commands.Packages;

/// <summary>Представляет команду изменения местоположения посылки.</summary>
/// <param name="Id">Идентификатор посылки.</param>
/// <param name="Address">Местоположение посылки для обновления.</param>
public record ChangePackageAddressCommand(Guid Id, Address Address) : ICommand<bool>;