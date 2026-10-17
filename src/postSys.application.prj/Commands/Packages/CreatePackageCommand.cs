using PostSys.Application.Contracts;
using PostSys.Application.Cqrs.Core;

using PostSys.Domain.Contracts;

namespace PostSys.Application.Commands.Packages;

/// <summary>Представляет команду создания посылки.</summary>
/// <param name="Dimensions">Параметры посылки.</param>
/// <param name="Address">Адрес.</param>
/// <param name="Status">Статус посылки.</param>
/// <param name="ClientId">Идентификатор клиента.</param>
/// <param name="PostmanId">Идентификатор почтальона.</param>
public record CreatePackageCommand(
	DimensionsDto Dimensions,
	AddressDto Address,
	PackageStatus Status,
	Guid ClientId,
	Guid PostmanId) : ICommand<Guid>;
