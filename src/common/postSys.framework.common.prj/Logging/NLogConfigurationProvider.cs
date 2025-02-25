using System;
using System.IO;

using NLog;
using NLog.Config;
using NLog.Targets;

namespace PostSys.Framework.Common.Logging;

/// <summary>Базовый провайдер конфигурации логирования NLog.</summary>
public static class NLogConfigurationProvider
{
	#region Methods

	/// <summary>Настраивает NLog.</summary>
	/// <param name="logDirPath">Базовый путь для сохранения логов.</param>
	public static void ConfigureNLog(string logDirPath)
	{
		var config = new LoggingConfiguration();

		var consoleTarget = new ConsoleTarget("console");
		var debugFileTarget = new FileTarget("debugFile")
		{
			FileName = Path.Combine(logDirPath, $"{DateTime.Now:yyyy-MM-dd}.dbg_.log"),
			ArchiveEvery = FileArchivePeriod.Day,
			MaxArchiveFiles = 30,
			Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}"
		};
		var errorFileTarget = new FileTarget("errorFile")
		{
			FileName = Path.Combine(logDirPath, $"{DateTime.Now:yyyy-MM-dd}.err_.log"),
			ArchiveEvery = FileArchivePeriod.Day,
			MaxArchiveFiles = 30,
			Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}"
		};

		config.AddRule(LogLevel.Debug, LogLevel.Fatal, debugFileTarget);
		config.AddRule(LogLevel.Error, LogLevel.Fatal, errorFileTarget);
		config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTarget);

		LogManager.Configuration = config;
	}

	#endregion
}