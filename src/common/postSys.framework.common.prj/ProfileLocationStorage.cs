using System.IO;

namespace PostSys.Framework.Common;

/// <summary>Хранилище профиля приложения.</summary>
public static class ProfileLocationStorage
{
	static ProfileLocationStorage()
	{
		ProfileRootDir = PathConstants.AppProgramDataPath;
	}

	/// <summary>Возвращает путь, куда будет сохраняться конфигурация приложения.</summary>
	/// <value>Путь, куда будет сохраняться конфигурация приложения.</value>
	public static string ConfigDirPath
	{
		get
		{
			var appCurrentDirectoryPath = PathConstants.AppCurrentDirectoryPath;
			var binIndex = appCurrentDirectoryPath.IndexOf(Path.DirectorySeparatorChar + "bin");

			if(binIndex < 0)
			{
				return appCurrentDirectoryPath;
			}

			return appCurrentDirectoryPath[..binIndex];
		}
	}

	/// <summary>Возвращает или задаёт корневой путь к корню профиля приложения.</summary>
	/// <value>Корневой путь к корню профиля приложения.</value>
	public static string ProfileRootDir { get; set; }

	/// <summary>Возвращает путь, куда будут сохраняться логи приложения.</summary>
	/// <value>Путь, куда будут сохраняться логи приложения.</value>
	public static string LogDirPath => Path.Combine(ProfileRootDir, "Logs");

	/// <summary>Возвращает имя файла, в котором будет содержаться конфигурация приложения.</summary>
	/// <value>Имя файла, в котором будет содержаться конфигурация приложения.</value>
	public static string ConfigFileName => "appsettings.json";

	/// <summary>Возвращает путь, где будут храниться конфигурационные файлы.</summary>
	/// <value>Путь, где будут храниться конфигурационные файлы.</value>
	public static string ConfigPath => Path.Combine(ConfigDirPath, ConfigFileName);
}
