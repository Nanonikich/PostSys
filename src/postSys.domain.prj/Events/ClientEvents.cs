using PostSys.Domain.Abstractions;
using PostSys.Domain.ValueObjects;

namespace PostSys.Domain.Events;

/// <summary>Клиент создан.</summary>
public sealed record ClientCreated(Guid ClientId) : IDomainEvent;

/// <summary>Изменено ФИО клиента.</summary>
public sealed record ClientFullnameChanged(Guid ClientId, Fullname Fullname) : IDomainEvent;

/// <summary>Изменён телефон клиента.</summary>
public sealed record ClientPhoneNumberChanged(Guid ClientId, string PhoneNumber) : IDomainEvent;

/// <summary>Клиент удалён.</summary>
public sealed record ClientDeleted(Guid ClientId) : IDomainEvent;
