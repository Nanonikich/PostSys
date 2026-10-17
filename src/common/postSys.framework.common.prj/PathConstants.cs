using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

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

	/// <summary>Путь к текущей директории приложения.</summary>
	private static readonly string CurrentDirectoryPath = Environment.CurrentDirectory;

	#endregion

	#region Properties

	/// <summary>Возвращает путь, по которому будут сохраняться данные приложения.</summary>
	/// <value>Путь, по которому будут сохраняться данные приложения.</value>
	/// <remarks>
	/// Приоритет: <c>POSTSYS_DATA_DIR</c> ? writable app data ? <see cref="AppContext.BaseDirectory"/>/data.
	/// На Linux в контейнере задавайте <c>POSTSYS_DATA_DIR</c> (например <c>/app/data</c>).
	/// </remarks>
	public static string AppProgramDataPath
	{
		get
		{
			var fromEnv = Environment.GetEnvironmentVariable("POSTSYS_DATA_DIR");
			if(!string.IsNullOrWhiteSpace(fromEnv))
				return fromEnv;

			var appName = Path.GetFileNameWithoutExtension(VersionInfo?.InternalName) ?? "Untitled";

			if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				return Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
					"PostSys",
					appName);
			}

			// Linux/macOS: CommonApplicationData часто /usr/share (не writable) — предпочитаем ~/.local/share.
			var userData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			if(!string.IsNullOrWhiteSpace(userData))
				return Path.Combine(userData, "PostSys", appName);

			return Path.Combine(AppContext.BaseDirectory, "data");
		}
	}

	/// <summary>Возвращает путь к текущей директории приложения.</summary>
	/// <value>Путь к текущей директории приложения.</value>
	public static string AppCurrentDirectoryPath => Path.Combine(
		CurrentDirectoryPath,
		Path.GetFileNameWithoutExtension(VersionInfo?.InternalName) ?? "Untitled");

	#endregion
}
