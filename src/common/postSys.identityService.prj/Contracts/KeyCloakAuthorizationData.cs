namespace PostSys.IdentityService.Contracts;

/// <summary>Данные для входа в KeyCloak.</summary>
public record KeyCloakAuthorizationData
{
	#region Properties

	/// <summary>Идентификатор пользователя KeyCloak.</summary>
	public required string ClientId { get; init; }

	/// <summary>Секрет пользователя.</summary>
	public required string ClientSecret { get; init; }

	/// <summary>Область.</summary>
	public required string Realm { get; init; }

	/// <summary>Путь для обращения к KeyCloak.</summary>
	public required string KeyCloakUrl { get; init; }

	#endregion
}