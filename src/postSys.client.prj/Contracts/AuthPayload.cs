using System;

namespace PostSys.Client.Contracts;

/// <summary>Данные ответа операции входа почтальона.</summary>
public sealed class AuthPayload
{
    /// <summary>Идентификатор почтальона.</summary>
    public Guid PostmanId { get; set; }

    /// <summary>Ключ доступа к службе.</summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>Момент окончания действия ключа доступа.</summary>
    public DateTimeOffset ExpiresAt { get; set; }
}