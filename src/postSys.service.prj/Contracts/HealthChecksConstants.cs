namespace PostSys.Service.Common.Contracts;

/// <summary>Константы проверок состояния службы.</summary>
public static class HealthChecksConstants
{
    /// <summary>Тег проверки жизнеспособности процесса.</summary>
    public const string LivenessTag = "Liveness";

    /// <summary>Путь проверки жизнеспособности.</summary>
    public const string LivenessUrlPattern = "/liveness";

    /// <summary>Тег проверки готовности к приёму запросов.</summary>
    public const string ReadinessTag = "Readiness";

    /// <summary>Путь проверки готовности.</summary>
    public const string ReadinessUrlPattern = "/readiness";
}