using PostSys.Common.Data;
using PostSys.Domain.Abstractions;
using PostSys.Domain.Events;
using PostSys.Domain.Postmen.Checkers;
using PostSys.Domain.ValueObjects;

namespace PostSys.Domain.Postmen;

/// <summary>??????? ??????????.</summary>
public class Postman : AggregateRoot, ISoftDelete
{
	/// <summary>?????????? ?????? ?? ????? ??????????.</summary>
	public Fullname Fullname { get; private set; } = null!;

	/// <summary>?????????? ????????????? ???????, ???????? ???????????.</summary>
	public Guid PostmanPackageId { get; private set; }

	/// <summary>?????????? email ??????????.</summary>
	public Email Email { get; private set; } = null!;

	/// <summary>?????????? ??? ?????? ??????????.</summary>
	public PasswordHash Password { get; private set; } = null!;

	/// <summary>?????????? ???? ???????? ??????????.</summary>
	public bool IsDeleted { get; private set; }

	private Postman()
	{
	}

	private Postman(
		Fullname fullname,
		Guid postmanPackageId,
		Email email,
		PasswordHash password)
	{
		Id = Guid.NewGuid();
		Fullname = fullname ?? throw new ArgumentNullException(nameof(fullname));
		PostmanPackageId = postmanPackageId;
		Email = email ?? throw new ArgumentNullException(nameof(email));
		Password = password ?? throw new ArgumentNullException(nameof(password));
	}

	/// <summary>??????? ?????? ??????????.</summary>
	public static async Task<Postman> CreateAsync(
		Fullname fullname,
		Guid packageId,
		Email email,
		PasswordHash password,
		IPostmanParametersChecker checker)
	{
		if(packageId != Guid.Empty && !await checker.CheckExistenceOfPackageAsync(packageId))
			throw new ArgumentException("Package does not exist!");

		if(!await checker.CheckPackageIsNotInOperationAsync(packageId))
			throw new ArgumentException("Package is in the work of another postman!");

		if(!await checker.CheckUniqueEmailAsync(email.Value))
			throw new ArgumentException("Email must be unique!");

		var postman = new Postman(fullname, packageId, email, password);
		postman.Raise(new PostmanCreated(postman.Id));
		return postman;
	}

	/// <summary>???????? ??????????? ???????.</summary>
	public async Task ChangeAsync(Guid newPackageId, IPostmanParametersChecker checker)
	{
		if(newPackageId != Guid.Empty && !await checker.CheckExistenceOfPackageAsync(newPackageId))
			throw new ArgumentException("Package does not exist!");

		if(!await checker.CheckPackageIsNotInOperationAsync(newPackageId))
			throw new ArgumentException("Package is in the work of another postman!");

		PostmanPackageId = newPackageId;
		Raise(new PostmanPackageChanged(Id, newPackageId));
	}

	/// <summary>???????? ??? ??????????.</summary>
	public void ChangeFullname(Fullname newFullname)
	{
		Fullname = newFullname ?? throw new ArgumentNullException(nameof(newFullname));
		Raise(new PostmanFullnameChanged(Id, Fullname));
	}

	/// <summary>???????? email ??????????.</summary>
	public async Task ChangeEmailAsync(Email newEmail, IPostmanParametersChecker checker)
	{
		ArgumentNullException.ThrowIfNull(newEmail);

		if(!await checker.CheckUniqueEmailAsync(newEmail.Value))
			throw new ArgumentException("Email must be unique!");

		Email = newEmail;
		Raise(new PostmanEmailChanged(Id, Email.Value));
	}

	/// <summary>???????? ??? ?????? ??????????.</summary>
	public void ChangePassword(PasswordHash newPassword)
	{
		Password = newPassword ?? throw new ArgumentNullException(nameof(newPassword));
		Raise(new PostmanPasswordChanged(Id));
	}

	/// <summary>??????? ?????????? (soft delete).</summary>
	public bool Delete()
	{
		if(IsDeleted)
			return false;

		IsDeleted = true;
		Raise(new PostmanDeleted(Id));
		return true;
	}
}
