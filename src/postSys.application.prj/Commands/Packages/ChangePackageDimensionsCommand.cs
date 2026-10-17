using PostSys.Application.Contracts;
using PostSys.Application.Cqrs.Core;

namespace PostSys.Application.Commands.Packages;

/// <summary>Представляет команду изменения параметров посылки.</summary>
/// <param name="Id">Идентификатор посылки.</param>
/// <param name="Dimensions">Параметры посылки для обновления.</param>
public record ChangePackageDimensionsCommand(Guid Id, DimensionsDto Dimensions) : ICommand<bool>;
