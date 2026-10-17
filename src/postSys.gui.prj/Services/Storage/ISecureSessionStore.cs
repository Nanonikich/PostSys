using System;
using System.Threading;
using System.Threading.Tasks;

namespace PostSys.Gui.Services.Storage;

/// <summary>Сохранённый сеанс пользователя.</summary>
/// <param name="Email">Адрес электронной почты.</param>
/// <param name="AccessToken">Ключ доступа к службе.</param>
/// <param name="PostmanId">Идентификатор почтальона.</param>
/// <param name="ExpiresAt">Момент окончания действия ключа доступа.</param>
/// <param name="PasswordHintEncrypted">Зарезервировано; не используется.</param>
public sealed record SecureSession(
    string Email,
    string AccessToken,
    Guid PostmanId,
    DateTimeOffset ExpiresAt,
    string? PasswordHintEncrypted);

/// <summary>Защищённое хранилище сеанса и учётных данных.</summary>
public interface ISecureSessionStore
{
    /// <summary>Сохраняет сеанс и при необходимости пароль.</summary>
    /// <param name="session">Данные сеанса.</param>
    /// <param name="passwordToRemember">Пароль для повторного входа или <see langword="null"/>.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача сохранения.</returns>
    Task SaveAsync(SecureSession session, string? passwordToRemember, CancellationToken cancellationToken = default);

    /// <summary>Загружает сохранённый сеанс.</summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сеанс или <see langword="null"/>, если данных нет.</returns>
    Task<SecureSession?> LoadAsync(CancellationToken cancellationToken = default);

    /// <summary>Загружает сохранённые учётные данные.</summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Пара «эл. почта / пароль» или <see langword="null"/>.</returns>
    Task<(string Email, string Password)?> LoadCredentialsAsync(CancellationToken cancellationToken = default);

    /// <summary>Удаляет сохранённый сеанс.</summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача очистки.</returns>
    Task ClearAsync(CancellationToken cancellationToken = default);
}