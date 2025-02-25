using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace PostSys.Framework.Common;

/// <summary>Константы путей файлов.</summary>
public static class PathConstants
{
	#region Data

	/// <summary>Сборка, которая начала исполнение приложения.</summary>
	private static readonly Assembly CallingAssembly = Assembly.GetEntryAssembly()!;

	/// <summary>Информация о файла сборки, которая начала исполнение приложения.</summary>
	private static readonly FileVersionInfo VersionInfo = FileVersionInfo.GetVersionInfo(
		CallingAssembly.Location);

	/// <summary>Путь к общей для всех пользователей в системе папке, где хранятся данные приложения.</summary>
	private static readonly string ProgramDataPath = Environment.GetFolderPath(
		Environment.SpecialFolder.CommonApplicationData);

	/// <summary>Путь к текущей директории приложения.</summary>
	private static readonly string CurrentDirectoryPath = Environment.CurrentDirectory;

	#endregion

	#region Properties

	/// <summary>Возвращает путь, по которому будут сохраняться данные приложения.</summary>
	/// <value>Путь, по которому будут сохраняться данные приложения.</value>
	public static string AppProgramDataPath => Path.Combine(
		ProgramDataPath,
		"PostSys",
		Path.GetFileNameWithoutExtension(VersionInfo?.InternalName) ?? "Untitled");

	/// <summary>Возвращает путь к текущей директории приложения.</summary>
	/// <value>Путь к текущей директории приложения.</value>
	public static string AppCurrentDirectoryPath => Path.Combine(
		CurrentDirectoryPath,
		Path.GetFileNameWithoutExtension(VersionInfo?.InternalName) ?? "Untitled");

	#endregion
}
