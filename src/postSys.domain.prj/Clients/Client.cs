using PostSys.Common.Data;
using PostSys.Domain.Abstractions;
using PostSys.Domain.Events;
using PostSys.Domain.ValueObjects;

namespace PostSys.Domain.Clients;

/// <summary>Агрегат клиента.</summary>
public class Client : AggregateRoot, ISoftDelete
{
    /// <summary>Возвращает данные об имени клиента.</summary>
    public Fullname Fullname { get; private set; } = null!;

    /// <summary>Возвращает номер телефона клиента.</summary>
    public PhoneNumber PhoneNumber { get; private set; } = null!;

    /// <summary>Возвращает флаг удаления клиента.</summary>
    public bool IsDeleted { get; private set; }

    private Client() { }

    private Client(Fullname fullname, PhoneNumber phoneNumber)
    {
        Id = Guid.NewGuid();
        Fullname = fullname ?? throw new ArgumentNullException(nameof(fullname));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
    }

    /// <summary>Создаёт нового клиента.</summary>
    public static Client Create(Fullname fullname, PhoneNumber phoneNumber)
    {
        var client = new Client(fullname, phoneNumber);
        client.Raise(new ClientCreated(client.Id));
        return client;
    }

    /// <summary>Изменяет имя клиента.</summary>
    public void ChangeFullname(Fullname newFullname)
    {
        Fullname = newFullname ?? throw new ArgumentNullException(nameof(newFullname));
        Raise(new ClientFullnameChanged(Id, Fullname));
    }

    /// <summary>Изменяет номер телефона клиента.</summary>
    public void ChangePhoneNumber(PhoneNumber newPhoneNumber)
    {
        PhoneNumber = newPhoneNumber ?? throw new ArgumentNullException(nameof(newPhoneNumber));
        Raise(new ClientPhoneNumberChanged(Id, PhoneNumber.Value));
    }

    /// <summary>Удаляет клиента (soft delete).</summary>
    public bool Delete()
    {
        if(IsDeleted)
            return false;

        IsDeleted = true;
        Raise(new ClientDeleted(Id));
        return true;
    }
}