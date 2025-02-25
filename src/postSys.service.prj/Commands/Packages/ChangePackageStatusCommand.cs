using System;

using PostSys.ReadModels.Contracts;
using PostSys.Service.Common.Cqrs.Core;

namespace PostSys.Service.Commands.Packages;

/// <summary>Представляет команду изменения статуса посылки.</summary>
/// <param name="Id">Идентификатор посылки.</param>
/// <param name="Status">Статус посылки для обновления.</param>
public record ChangePackageStatusCommand(Guid Id, PackageStatus Status) : ICommand<bool>;
