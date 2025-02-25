using CSharpFunctionalExtensions;

using PostSys.Common.Data;
using PostSys.Domain.Packages;
using PostSys.Domain.ValueObjects;

namespace PostSys.Domain.Clients;

/// <summary>Модель, описывающая клиента.</summary>
public class Client : Entity<Guid>, ISoftDelete
{
	#region Properties

	/// <summary>Возвращает или задаёт данные об имени клиента.</summary>
	/// <value>Данные об имени клиента.</value>
	public Fullname Fullname { get; private set; }

	/// <summary>Возвращает или задаёт номер телефона клиента.</summary>
	/// <value>Номер телефона клиента.</value>
	public PhoneNumber PhoneNumber { get; private set; }

	/// <summary>Возвращает или задаёт флаг удаления клиента.</summary>
	/// <value>Флаг удаления клиента.</value>
	public bool IsDeleted { get; private set; }

	/// <summary>Возвращает или задаёт навигационное свойство для связи с посылками.</summary>
	/// <value>Навигационное свойство для связи с посылками.</value>
	public ICollection<Package> Packages { get; private set; } = [];

	#endregion

	#region .ctor

	/// <summary>Конструктор по умолчанию.</summary>
	private Client()
	{
	}

	/// <summary>Создаёт экземпляр класса <see cref="Client"/>.</summary>
	/// <param name="fullname">Данные об имени клиента.</param>
	/// <param name="phoneNumber">Номер телефона клиента.</param>
	private Client(Fullname fullname, PhoneNumber phoneNumber)
	{
		Fullname = fullname ?? throw new ArgumentNullException(nameof(fullname));
		PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
	}

	#endregion

	#region Methods

	/// <summary>Создаёт нового клиента с указанными данными.</summary>
	/// <param name="fullname">Данные об имени клиента.</param>
	/// <param name="phoneNumber">Номер телефона клиента.</param>
	public static Client Create(Fullname fullname, PhoneNumber phoneNumber)
	{
		return new Client(fullname, phoneNumber);
	}

	/// <summary>Изменяет имя клиента.</summary>
	/// <param name="newFullname">Новые данные об имени клиента.</param>
	public void ChangeFullname(Fullname newFullname)
	{
		Fullname = newFullname ?? throw new ArgumentNullException(nameof(newFullname));
	}

	/// <summary>Изменяет номер телефона клиента.</summary>
	/// <param name="newPhoneNumber">Новый номер телефона клиента.</param>
	public void ChangePhoneNumber(PhoneNumber newPhoneNumber)
	{
		PhoneNumber = newPhoneNumber ?? throw new ArgumentNullException(nameof(newPhoneNumber));
	}
	
	#region ISoftDelete

	/// <summary>Удаляет клиента (soft delete).</summary>
	/// <returns>Удалось ли выполнить операцию удаления.</returns>
	public bool Delete()
	{
		if(!IsDeleted)
		{
			IsDeleted = true;
			return true;
		}
		
		return false;
	}

	#endregion

	#endregion
}