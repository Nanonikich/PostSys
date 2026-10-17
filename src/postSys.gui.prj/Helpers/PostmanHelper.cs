using System;

namespace PostSys.Gui.Helpers;

/// <summary>Сессия авторизованного почтальона.</summary>
public sealed class PostmanHelper
{
    /// <summary>Идентификатор почтальона.</summary>
    public Guid PostmanId { get; set; }

    /// <summary>Ключ доступа к службе.</summary>
    public string? AccessToken { get; set; }

    /// <summary>Момент окончания действия ключа доступа.</summary>
    public DateTimeOffset? TokenExpiresAt { get; set; }

    /// <summary>Адрес электронной почты последнего успешного входа.</summary>
    public string? Email { get; set; }

    /// <summary>Сбрасывает сессию.</summary>
    public void Clear()
    {
        PostmanId = Guid.Empty;
        AccessToken = null;
        TokenExpiresAt = null;
        Email = null;
    }
}