namespace PostSys.Service.Auth;

/// <summary>Параметры выдачи ключей доступа.</summary>
public sealed class JwtOptions
{
    /// <summary>Имя секции в конфигурации приложения.</summary>
    public const string SectionName = "Jwt";

    /// <summary>Секрет подписи ключей (не менее 32 символов).</summary>
    public string SigningKey { get; set; } = "PostSys-Dev-Signing-Key-Change-Me-32chars!";

    /// <summary>Издатель ключей доступа.</summary>
    public string Issuer { get; set; } = "PostSys";

    /// <summary>Получатель ключей доступа.</summary>
    public string Audience { get; set; } = "PostSys.Gui";

    /// <summary>Срок действия ключа доступа в минутах.</summary>
    public int ExpiryMinutes { get; set; } = 480;
}