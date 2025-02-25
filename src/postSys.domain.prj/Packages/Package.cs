using CSharpFunctionalExtensions;

using PostSys.Common.Data;
using PostSys.Domain.Clients;
using PostSys.Domain.Packages.Checkers;
using PostSys.Domain.Postmen;
using PostSys.Domain.ValueObjects;

namespace PostSys.Domain.Packages;

/// <summary>Модель, описывающая посылку.</summary>
public class Package : Entity<Guid>, ISoftDelete
{
	#region Properties

	/// <summary>Возвращает или задаёт параметры посылки.</summary>
	/// <value>Параметры посылки.</value>
	public Dimensions Dimensions { get; private set; }

	/// <summary>Возвращает или задаёт адрес посылки.</summary>
	/// <value>Адрес посылки.</value>
	public Address Address { get; private set; }

	/// <summary>Возвращает или задаёт статус посылки.</summary>
	/// <value>Статус посылки.</value>
	public PackageStatus Status { get; private set; }

	/// <summary>Возвращает или задаёт идентификатор клиента, отправившего посылку.</summary>
	/// <value>Идентификатор клиента, отправившего посылку.</value>
	public Guid PackageClientId { get; private set; }

	/// <summary>Возвращает или задаёт идентификатор почтальона, доставляющего посылку.</summary>
	/// <value>Идентификатор почтальона, доставляющего посылку.</value>
	public Guid PackagePostmanId { get; private set; }

	/// <summary>Возвращает или задаёт флаг удаления посылки.</summary>
	/// <value>Флаг удаления посылки.</value>
	public bool IsDeleted { get; private set; }

	/// <summary>Возвращает или задаёт навигационное свойство связи с клиентом.</summary>
	/// <value>Навигационное свойство связи с клиентом.</value>
	public Client Client { get; }

	/// <summary>Возвращает или задаёт навигационное свойство связи с почтальоном.</summary>
	/// <value>Навигационное свойство связи с почтальоном.</value>
	public Postman Postman { get; }

	#endregion

	#region .ctor

	/// <summary>Конструктор по умолчанию.</summary>
	private Package()
	{
	}

	/// <summary>Создаёт экземпляр класса <see cref="Package"/>.</summary>
	/// <param name="dimensions">Параметры посылки.</param>
	/// <param name="address">Адрес.</param>
	/// <param name="status">Статус посылки.</param>
	/// <param name="packageClientId">Идентификатор клиента.</param>
	/// <param name="packagePostmanId">Идентификатор почтальона.</param>
	private Package(
		Dimensions dimensions,
		Address address,
		PackageStatus status,
		Guid packageClientId,
		Guid packagePostmanId)
	{
		Dimensions = dimensions ?? throw new ArgumentNullException(nameof(dimensions));
		Address = address ?? throw new ArgumentNullException(nameof(address));
		Status = status ?? throw new ArgumentNullException(nameof(status));
		PackageClientId = packageClientId;
		PackagePostmanId = packagePostmanId;
	}

	#endregion

	#region Methods

	/// <summary>Создаёт новою посылку с указанными данными.</summary>
	/// <param name="dimensions">Параметры посылки.</param>
	/// <param name="address">Адрес.</param>
	/// <param name="status">Статус.</param>
	/// <param name="packageClientId">Идентификатор клиента.</param>
	/// <param name="packagePostmanId">Идентификатор почтальона.</param>
	/// <param name="checker"><see cref="IPackageParametersChecker"/>.</param>
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

		return new Package(
			dimensions,
			address,
			status,
			packageClientId,
			packagePostmanId);
	}

	/// <summary>Изменяет данные о посылке.</summary>
	/// <param name="clientId">Идентификатор клиента.</param>
	/// <param name="postmanId">Идентификатор почтальона.</param>
	/// <param name="checker"><see cref="IPackageParametersChecker"/>.</param>
	public async Task ChangeAsync(Guid clientId, Guid postmanId, IPackageParametersChecker checker)
	{
		if(!await checker.CheckExistenceOfClientAsync(clientId)) throw new ArgumentException("Client does not exist!");

		if(!await checker.CheckExistenceOfPostmanAsync(postmanId))
			throw new ArgumentException("Postman does not exist!");

		PackageClientId = clientId;
		PackagePostmanId = postmanId;
	}

	/// <summary>Изменяет параметры посылки.</summary>
	/// <param name="newDimensions">Новые параметры посылки.</param>
	public void ChangeDimensions(Dimensions newDimensions)
	{
		Dimensions = newDimensions ?? throw new ArgumentNullException(nameof(newDimensions));
	}

	/// <summary>Изменяет адрес посылки.</summary>
	/// <param name="newAddress">Новый адрес посылки.</param>
	public void ChangeAddress(Address newAddress)
	{
		Address = newAddress ?? throw new ArgumentNullException(nameof(newAddress));
	}

	/// <summary>Изменяет статус посылки.</summary>
	/// <param name="newStatus">Новый статус посылки.</param>
	public void ChangeStatus(PackageStatus newStatus)
	{
		Status = newStatus ?? throw new ArgumentNullException(nameof(newStatus));
	}

	#region ISoftDelete

	/// <summary>Удаляет посылку (soft delete).</summary>
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