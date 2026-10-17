namespace PostSys.Service.Common.Contracts;

/// <summary>Представляет набор констант для работы с проверками состояния.</summary>
public static class HealthChecksConstants
{
	#region Constants

	/// <summary>Хранит тег Liveness-пробы.</summary>
	public const string LivenessTag = "Liveness";

	/// <summary>Хранит URL для Liveness-пробы.</summary>
	public const string LivenessUrlPattern = "/liveness";

	/// <summary>Хранит тег Readiness-пробы.</summary>
	public const string ReadinessTag = "Readiness";

	/// <summary>Хранит URL для Readiness-пробы.</summary>
	public const string ReadinessUrlPattern = "/readiness";

	#endregion
}