using System;

using PostSys.ReadModels.Contracts;
using PostSys.ReadModels.Helpers;
using PostSys.Service.Common.Cqrs.Core;

namespace PostSys.Service.Commands.Packages;

/// <summary>Представляет команду создания клиента.</summary>
/// <param name="Dimensions">Параметры посылки.</param>
/// <param name="Address">Адрес.</param>
/// <param name="Status">Статус посылки для обновления.</param>
/// <param name="ClientId">Идентификатор клиента.</param>
/// <param name="PostmanId">Идентификатор почтальона.</param>
public record CreatePackageCommand(
	Dimensions Dimensions,
	Address Address,
	PackageStatus Status,
	Guid ClientId,
	Guid PostmanId) : ICommand<Guid>;