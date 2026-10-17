using PostSys.Common.Data;
using PostSys.Domain.Abstractions;
using PostSys.Domain.Events;
using PostSys.Domain.Postmen.Checkers;
using PostSys.Domain.ValueObjects;

namespace PostSys.Domain.Postmen;

/// <summary>Агрегат почтальона.</summary>
public class Postman : AggregateRoot, ISoftDelete
{
    /// <summary>ФИО почтальона.</summary>
    public Fullname Fullname { get; private set; } = null!;

    /// <summary>Идентификатор текущей назначенной посылки (или пустой).</summary>
    public Guid PostmanPackageId { get; private set; }

    /// <summary>Адрес электронной почты почтальона.</summary>
    public Email Email { get; private set; } = null!;

    /// <summary>Хэш пароля почтальона.</summary>
    public PasswordHash Password { get; private set; } = null!;

    /// <summary>Признак мягкого удаления.</summary>
    public bool IsDeleted { get; private set; }

    private Postman() { }

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

    /// <summary>Создаёт нового почтальона.</summary>
    /// <param name="fullname">ФИО.</param>
    /// <param name="packageId">Идентификатор назначаемой посылки или пустой.</param>
    /// <param name="email">Адрес электронной почты.</param>
    /// <param name="password">Хэш пароля.</param>
    /// <param name="checker">Проверки параметров почтальона.</param>
    /// <returns>Созданный агрегат.</returns>
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

    /// <summary>Назначает почтальону другую посылку.</summary>
    /// <param name="newPackageId">Идентификатор посылки или пустой.</param>
    /// <param name="checker">Проверки параметров почтальона.</param>
    /// <returns>Задача смены назначения.</returns>
    public async Task ChangeAsync(Guid newPackageId, IPostmanParametersChecker checker)
    {
        if(newPackageId != Guid.Empty && !await checker.CheckExistenceOfPackageAsync(newPackageId))
            throw new ArgumentException("Package does not exist!");

        if(!await checker.CheckPackageIsNotInOperationAsync(newPackageId))
            throw new ArgumentException("Package is in the work of another postman!");

        PostmanPackageId = newPackageId;
        Raise(new PostmanPackageChanged(Id, newPackageId));
    }

    /// <summary>Изменяет ФИО почтальона.</summary>
    /// <param name="newFullname">Новое ФИО.</param>
    public void ChangeFullname(Fullname newFullname)
    {
        Fullname = newFullname ?? throw new ArgumentNullException(nameof(newFullname));
        Raise(new PostmanFullnameChanged(Id, Fullname));
    }

    /// <summary>Изменяет адрес электронной почты.</summary>
    /// <param name="newEmail">Новый адрес.</param>
    /// <param name="checker">Проверки параметров почтальона.</param>
    /// <returns>Задача смены адреса.</returns>
    public async Task ChangeEmailAsync(Email newEmail, IPostmanParametersChecker checker)
    {
        ArgumentNullException.ThrowIfNull(newEmail);

        if(!await checker.CheckUniqueEmailAsync(newEmail.Value))
            throw new ArgumentException("Email must be unique!");

        Email = newEmail;
        Raise(new PostmanEmailChanged(Id, Email.Value));
    }

    /// <summary>Изменяет хэш пароля.</summary>
    /// <param name="newPassword">Новый хэш пароля.</param>
    public void ChangePassword(PasswordHash newPassword)
    {
        Password = newPassword ?? throw new ArgumentNullException(nameof(newPassword));
        Raise(new PostmanPasswordChanged(Id));
    }

    /// <summary>Удаляет почтальона (мягкое удаление).</summary>
    /// <returns><see langword="true"/>, если удаление выполнено.</returns>
    public bool Delete()
    {
        if(IsDeleted)
            return false;

        IsDeleted = true;
        Raise(new PostmanDeleted(Id));
        return true;
    }
}