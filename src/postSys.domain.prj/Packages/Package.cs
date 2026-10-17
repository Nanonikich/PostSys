using PostSys.Common.Data;
using PostSys.Domain.Abstractions;
using PostSys.Domain.Events;
using PostSys.Domain.Packages.Checkers;
using PostSys.Domain.ValueObjects;

namespace PostSys.Domain.Packages;

/// <summary>Агрегат посылки.</summary>
public class Package : AggregateRoot, ISoftDelete
{
    /// <summary>Возвращает параметры посылки.</summary>
    public Dimensions Dimensions { get; private set; } = null!;

    /// <summary>Возвращает адрес посылки.</summary>
    public Address Address { get; private set; } = null!;

    /// <summary>Возвращает статус посылки.</summary>
    public PackageStatus Status { get; private set; } = null!;

    /// <summary>Возвращает идентификатор клиента.</summary>
    public Guid PackageClientId { get; private set; }

    /// <summary>Возвращает идентификатор почтальона.</summary>
    public Guid PackagePostmanId { get; private set; }

    /// <summary>Возвращает флаг удаления посылки.</summary>
    public bool IsDeleted { get; private set; }

    private Package() { }

    private Package(
        Dimensions dimensions,
        Address address,
        PackageStatus status,
        Guid packageClientId,
        Guid packagePostmanId)
    {
        Id = Guid.NewGuid();
        Dimensions = dimensions ?? throw new ArgumentNullException(nameof(dimensions));
        Address = address ?? throw new ArgumentNullException(nameof(address));
        Status = status ?? throw new ArgumentNullException(nameof(status));
        PackageClientId = packageClientId;
        PackagePostmanId = packagePostmanId;
    }

    /// <summary>Создаёт новую посылку.</summary>
    public static async Task<Package> CreateAsync(
        Dimensions dimensions,
        Address address,
        PackageStatus status,
        Guid packageClientId,
        Guid packagePostmanId,
        IPackageParametersChecker checker)
    {
        if(!await checker.CheckExistenceOfClientAsync(packageClientId))
            throw new ArgumentException("Client does not exist!");

        if(!await checker.CheckExistenceOfPostmanAsync(packagePostmanId))
            throw new ArgumentException("Postman does not exist!");

        var package = new Package(dimensions, address, status, packageClientId, packagePostmanId);
        package.Raise(new PackageCreated(package.Id));
        return package;
    }

    /// <summary>Изменяет клиента и почтальона посылки.</summary>
    public async Task ChangeAsync(Guid clientId, Guid postmanId, IPackageParametersChecker checker)
    {
        if(!await checker.CheckExistenceOfClientAsync(clientId))
            throw new ArgumentException("Client does not exist!");

        if(!await checker.CheckExistenceOfPostmanAsync(postmanId))
            throw new ArgumentException("Postman does not exist!");

        PackageClientId = clientId;
        PackagePostmanId = postmanId;
        Raise(new PackageAssignmentsChanged(Id, clientId, postmanId));
    }

    /// <summary>Изменяет параметры посылки.</summary>
    public void ChangeDimensions(Dimensions newDimensions)
    {
        Dimensions = newDimensions ?? throw new ArgumentNullException(nameof(newDimensions));
        Raise(new PackageDimensionsChanged(Id, Dimensions));
    }

    /// <summary>Изменяет адрес посылки.</summary>
    public void ChangeAddress(Address newAddress)
    {
        Address = newAddress ?? throw new ArgumentNullException(nameof(newAddress));
        Raise(new PackageAddressChanged(Id, Address));
    }

    /// <summary>Изменяет статус посылки.</summary>
    public void ChangeStatus(PackageStatus newStatus)
    {
        Status = newStatus ?? throw new ArgumentNullException(nameof(newStatus));
        Raise(new PackageStatusChanged(Id, Status));
    }

    /// <summary>Удаляет посылку (soft delete).</summary>
    public bool Delete()
    {
        if(IsDeleted)
            return false;

        IsDeleted = true;
        Raise(new PackageDeleted(Id));
        return true;
    }
}