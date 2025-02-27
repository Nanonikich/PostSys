using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace PostSys.IdentityService.Contracts;

/// <summary>Ответ сервиса KeyCloak на запрос входа.</summary>
[DataContract]
internal sealed record OpenIdLoginResponse
{
	#region Properties

	/// <summary>Возвращает токен доступа.</summary>
	/// <value>Токен доступа.</value>
	[JsonPropertyName("access_token")]
	[DataMember]
	public required string AccessToken { get; init; }

	/// <summary>Возвращает время жизни <param cref="AccessToken"/> в секундах.</summary>
	/// <value>Время жизни <param cref="AccessToken"/> в секундах.</value>
	[JsonPropertyName("expires_in")]
	[DataMember]
	public required int ExpiresIn { get; init; }

	/// <summary>Возвращает время жизни <param cref="RefreshToken"/> в секундах.</summary>
	/// <value>Время жизни <param cref="RefreshToken"/> в секундах.</value>
	[JsonPropertyName("refresh_expires_in")]
	[DataMember]
	public required int RefreshExpiresIn { get; init; }

	/// <summary>Возвращает токен, для обновления времени жизни <param cref="AccessToken"/>.</summary>
	/// <value>Токен, для обновления времени жизни <param cref="AccessToken"/>.</value>
	[JsonPropertyName("refresh_token")]
	[DataMember]
	public required string RefreshToken { get; init; }

	/// <summary>Возвращает тип токена <param cref="AccessToken"/>.</summary>
	/// <value>Тип токена <param cref="AccessToken"/>.</value>
	[JsonPropertyName("token_type")]
	[DataMember]
	public required string TokenType { get; init; }

	/// <summary>Возвращает политику использования токена.</summary>
	/// <value>Политика использования токена.</value>
	[JsonPropertyName("not-before-policy")]
	[DataMember]
	public required int NotBeforePolicy { get; init; }

	/// <summary>Возвращает GUID состояния сессии.</summary>
	/// <value>GUID состояния сессии.</value>
	[JsonPropertyName("session_state")]
	[DataMember]
	public required Guid SessionState { get; init; }

	/// <summary>Возвращает данные, которые передаются в токене.</summary>
	/// <value>Данные, которые передаются в токене.</value>
	[JsonPropertyName("scope")]
	[DataMember]
	public required string Scope { get; init; }

	#endregion
}
