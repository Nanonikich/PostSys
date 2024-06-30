using System.Diagnostics;
using System.Reflection;

namespace PostSys.Application.Framework;
#nullable enable

/// <summary>Константы путей файлов.</summary>
public static class PathConstants
{
	#region Data

	/// <summary>Сборка, которая начала исполнение приложения.</summary>
	private static readonly Assembly CallingAssembly = Assembly.GetEntryAssembly()!;

	/// <summary>Информация о файла сборки, которая начала исполнение приложения.</summary>
	private static readonly FileVersionInfo VersionInfo = FileVersionInfo.GetVersionInfo(
		CallingAssembly.Location);

	/// <summary>
	/// Путь к общей для всех пользователей в системе папке, где хранятся данные приложения.
	/// </summary>
	private static readonly string ProgramDataPath = Environment.GetFolderPath(
		Environment.SpecialFolder.CommonApplicationData);

	/// <summary>
	/// Путь к каталогу, который служит общим репозиторием данных приложения, используемых текущим пользователем.
	/// </summary>
	private static readonly string ProgramLocalDataPath = Environment.GetFolderPath(
		Environment.SpecialFolder.LocalApplicationData);

	/// <summary>
	/// Путь к текущей директории приложения.
	/// </summary>
	private static readonly string CurrentDirectoryPath = Environment.CurrentDirectory;

	#endregion

	#region Properties

	/// <summary>Возвращает путь к общей для всех пользователей в системе папке, где хранятся данные приложения.</summary>
	/// <value>Путь, по которому будут сохраняться данные приложения.</value>
	public static string AppProgramDataPath => Path.Combine(
		ProgramDataPath,
		Path.GetFileNameWithoutExtension(VersionInfo?.InternalName) ?? "Untitled");

	/// <summary>Возвращает путь к каталогу, который служит общим репозиторием данных приложения, используемых текущим пользователем.</summary>
	/// <value>Путь, по которому будут сохраняться данные приложения.</value>
	public static string AppProgramLocalDataPath => Path.Combine(
		ProgramLocalDataPath,
		Path.GetFileNameWithoutExtension(VersionInfo?.InternalName) ?? "Untitled");

	/// <summary>Возвращает путь к текущей директории приложения.</summary>
	/// <value>Путь, по которому будут сохраняться данные приложения.</value>
	public static string AppCurrentDirectoryPath => Path.Combine(
		CurrentDirectoryPath,
		Path.GetFileNameWithoutExtension(VersionInfo?.InternalName) ?? "Untitled");

	/// <summary>Возвращает наименование запущенного приложения.</summary>
	/// <value>Наименование запущенного приложения.</value>
	public static string AppName => Path.GetFileNameWithoutExtension(VersionInfo.FileName);

	/// <summary>Возвращает путь, по которому будут сохраняться протоколы работы приложения.</summary>
	/// <value>Путь, по которому будут сохраняться протоколы работы приложения.</value>
	public static string LogsRootPath => Path.Combine(
		AppProgramDataPath,
		"Logs");

	/// <summary>Возвращает путь и название файла, с которым будут сохраняться протоколы работы приложения.</summary>
	/// <value>Путь и название файла, с которым будут сохраняться протоколы работы приложения.</value>
	public static string LogsPath => Path.Combine(
		LogsRootPath,
		Path.GetFileNameWithoutExtension(VersionInfo.FileName));

	/// <summary>Возвращает путь к папке временных файлов проекта.</summary>
	public static string TempDataPath => Path.Combine(
		AppProgramDataPath,
		"Temp");

	#endregion
}
