using PostSys.Domain.Abstractions;
using PostSys.Domain.ValueObjects;

namespace PostSys.Domain.Events;

/// <summary>Почтальон создан.</summary>
public sealed record PostmanCreated(Guid PostmanId) : IDomainEvent;

/// <summary>Изменена назначенная посылка почтальона.</summary>
public sealed record PostmanPackageChanged(Guid PostmanId, Guid PackageId) : IDomainEvent;

/// <summary>Изменено ФИО почтальона.</summary>
public sealed record PostmanFullnameChanged(Guid PostmanId, Fullname Fullname) : IDomainEvent;

/// <summary>Изменён email почтальона.</summary>
public sealed record PostmanEmailChanged(Guid PostmanId, string Email) : IDomainEvent;

/// <summary>Изменён пароль почтальона.</summary>
public sealed record PostmanPasswordChanged(Guid PostmanId) : IDomainEvent;

/// <summary>Почтальон удалён.</summary>
public sealed record PostmanDeleted(Guid PostmanId) : IDomainEvent;