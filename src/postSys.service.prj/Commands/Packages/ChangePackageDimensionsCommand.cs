using System;

using PostSys.ReadModels.Helpers;
using PostSys.Service.Common.Cqrs.Core;

namespace PostSys.Service.Commands.Packages;

/// <summary>Представляет команду изменения параметров посылки.</summary>
/// <param name="Id">Идентификатор посылки.</param>
/// <param name="Dimensions">Параметры посылки для обновления.</param>
public record ChangePackageDimensionsCommand(Guid Id, Dimensions Dimensions) : ICommand<bool>;