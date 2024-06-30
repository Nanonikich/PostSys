namespace PostSys.Application.Framework;

/// <summary>Хранилище профиля приложения.</summary>
public static class ProfileLocationStorage
{
	/// <summary>Возвращает или задаёт корневой путь к корню профиля приложения.</summary>
	/// <value>Корневой путь к корню профиля приложения.</value>
	public static string ProfileRootDir { get; set; }

	/// <summary>Возвращает путь, куда будут сохранятся логи приложения.</summary>
	/// <value>Путь, куда будут сохранятся логи приложения.</value>
	public static string LogDirPath => Path.Combine(ProfileRootDir, "Logs/Logs");

}
