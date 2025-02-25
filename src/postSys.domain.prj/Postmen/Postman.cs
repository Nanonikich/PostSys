using CSharpFunctionalExtensions;

using PostSys.Common.Data;
using PostSys.Domain.Packages;
using PostSys.Domain.Postmen.Checkers;
using PostSys.Domain.ValueObjects;

namespace PostSys.Domain.Postmen;

/// <summary>Модель, описывающая почтальона.</summary>
public class Postman : Entity<Guid>, ISoftDelete
{
	#region Properties

	/// <summary>Возвращает или задаёт данные об имени почтальона.</summary>
	/// <value>Данные об имени почтальона.</value>
	public Fullname Fullname { get; private set; }

	/// <summary>Возвращает или задаёт идентификатор посылки.</summary>
	/// <value>Идентификатор посылки.</value>
	public Guid PostmanPackageId { get; private set; }

	/// <summary>Возвращает или задаёт email почтальона для доступа в систему.</summary>
	/// <value>Email почтальона для доступа в систему.</value>
	public Email Email { get; private set; }

	/// <summary>Возвращает или задаёт пароль почтальона для доступа в систему.</summary>
	/// <value>Пароль почтальона для доступа в систему.</value>
	public Password Password { get; private set; }

	/// <summary>Возвращает или задаёт флаг удаления почтальона.</summary>
	/// <value>Флаг удаления почтальона.</value>
	public bool IsDeleted { get; private set; }

	/// <summary>Возвращает или задаёт навигационное свойство для связи с посылками.</summary>
	/// <value>Навигационное свойство для связи с посылками.</value>
	public ICollection<Package> Packages { get; private set; } = [];

	#endregion

	#region .ctor

	/// <summary>Конструктор по умолчанию.</summary>
	private Postman()
	{
	}

	/// <summary>Создаёт экземпляр класса <see cref="Postman"/>.</summary>
	/// <param name="fullname">Данные об имени почтальона.</param>
	/// <param name="postmanPackageId">Идентификатор посылки.</param>
	/// <param name="email">Email почтальона для доступа в систему.</param>
	/// <param name="password">Пароль почтальона для доступа в систему.</param>
	private Postman(
		Fullname fullname,
		Guid postmanPackageId,
		Email email,
		Password password)
	{
		Fullname = fullname ?? throw new ArgumentNullException(nameof(fullname));
		PostmanPackageId = postmanPackageId;
		Email = email ?? throw new ArgumentNullException(nameof(email));
		Password = password ?? throw new ArgumentNullException(nameof(password));
	}

	#endregion

	#region Methods

	/// <summary>Создаёт новою посылку с указанными данными.</summary>
	/// <param name="fullname">Данные об имени почтальона.</param>
	/// <param name="packageId">Идентификатор посылки.</param>
	/// <param name="email">Email почтальона для доступа в систему.</param>
	/// <param name="password">Пароль почтальона для доступа в систему.</param>
	/// <param name="checker"><see cref="IPostmanParametersChecker"/>.</param>
	public static async Task<Postman> CreateAsync(
		Fullname fullname,
		Guid packageId,
		Email email,
		Password password,
		IPostmanParametersChecker checker)
	{
		if(packageId != Guid.Empty && !await checker.CheckExistenceOfPackageAsync(packageId))
			throw new ArgumentException("Package does not exist!");

		if(!await checker.CheckPackageIsNotInOperationAsync(packageId))
			throw new ArgumentException("Package is in the work of another postman!");

		if(!await checker.CheckUniqueEmailAsync(email.Value)) throw new ArgumentException("Email must be unique!");

		return new Postman(fullname, packageId, email, password);
	}

	/// <summary>Изменяет данные почтальона.</summary>
	/// <param name="newPackageId">Новый идентификатор посылки.</param>
	/// <param name="checker"><see cref="IPostmanParametersChecker"/>.</param>
	public async Task ChangeAsync(Guid newPackageId, IPostmanParametersChecker checker)
	{
		if(newPackageId != Guid.Empty && !await checker.CheckExistenceOfPackageAsync(newPackageId))
			throw new ArgumentException("Package does not exist!");

		if(!await checker.CheckPackageIsNotInOperationAsync(newPackageId))
			throw new ArgumentException("Package is in the work of another postman!");

		PostmanPackageId = newPackageId;
	}

	/// <summary>Изменяет имя почтальона.</summary>
	/// <param name="newFullname">Новые данные об имени почтальона.</param>
	public void ChangeFullname(Fullname newFullname)
	{
		Fullname = newFullname ?? throw new ArgumentNullException(nameof(newFullname));
	}

	/// <summary>Изменяет Email почтальона.</summary>
	/// <param name="newEmail">Новый Email.</param>
	/// <param name="checker"><see cref="IPostmanParametersChecker"/>.</param>
	public async Task ChangeEmailAsync(Email newEmail, IPostmanParametersChecker checker)
	{
		ArgumentNullException.ThrowIfNull(nameof(newEmail));

		if(!await checker.CheckUniqueEmailAsync(newEmail.Value)) throw new ArgumentException("Email must be unique!");

		Email = newEmail;
	}

	/// <summary>Изменяет пароль почтальона.</summary>
	/// <param name="newPassword">Новый пароль почтальона.</param>
	public void ChangePassword(Password newPassword)
	{
		Password = newPassword ?? throw new ArgumentNullException(nameof(newPassword));
	}
	
	#region ISoftDelete

	/// <summary>Удаляет почтальона (soft delete).</summary>
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