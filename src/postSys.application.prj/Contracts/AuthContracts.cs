namespace PostSys.Application.Contracts;

/// <summary>Результат успешного входа почтальона.</summary>
/// <param name="PostmanId">Идентификатор почтальона.</param>
/// <param name="AccessToken">Ключ доступа к службе.</param>
/// <param name="ExpiresAt">Момент окончания действия ключа.</param>
public sealed record AuthResult(Guid PostmanId, string AccessToken, DateTimeOffset ExpiresAt);

/// <summary>Служба выдачи ключей доступа почтальонам.</summary>
public interface IJwtTokenService
{
    /// <summary>Создаёт ключ доступа.</summary>
    /// <param name="postmanId">Идентификатор почтальона.</param>
    /// <param name="email">Адрес электронной почты.</param>
    /// <returns>Результат входа с ключом доступа.</returns>
    AuthResult CreateToken(Guid postmanId, string email);
}