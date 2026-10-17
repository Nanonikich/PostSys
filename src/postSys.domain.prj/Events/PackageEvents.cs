using PostSys.Domain.Abstractions;
using PostSys.Domain.ValueObjects;

namespace PostSys.Domain.Events;

/// <summary>Посылка создана.</summary>
public sealed record PackageCreated(Guid PackageId) : IDomainEvent;

/// <summary>Изменены клиент/почтальон посылки.</summary>
public sealed record PackageAssignmentsChanged(Guid PackageId, Guid ClientId, Guid PostmanId) : IDomainEvent;

/// <summary>Изменены габариты посылки.</summary>
public sealed record PackageDimensionsChanged(Guid PackageId, Dimensions Dimensions) : IDomainEvent;

/// <summary>Изменён адрес посылки.</summary>
public sealed record PackageAddressChanged(Guid PackageId, Address Address) : IDomainEvent;

/// <summary>Изменён статус посылки.</summary>
public sealed record PackageStatusChanged(Guid PackageId, PackageStatus Status) : IDomainEvent;

/// <summary>Посылка удалена.</summary>
public sealed record PackageDeleted(Guid PackageId) : IDomainEvent;
