using PostSys.Application.Cqrs.Core;

using PostSys.Domain.Contracts;

namespace PostSys.Application.Commands.Packages;

/// <summary>Представляет команду изменения статуса посылки.</summary>
/// <param name="Id">Идентификатор посылки.</param>
/// <param name="Status">Статус посылки для обновления.</param>
public record ChangePackageStatusCommand(Guid Id, PackageStatus Status) : ICommand<bool>;