using Serilog.Core;
using Serilog.Events;
using Serilog;

namespace PostSys.Application.Framework;

/// <summary>Базовый провайдер конфигурации логирования Serilog.</summary>
public class BaseLoggerConfiguarationProvider
{
	///<summary>Возвращает уровень логирования.</summary>
	/// <value>Уровень логирования.</value>
	public static LoggingLevelSwitch LoggingLevel { get; } = new(LogEventLevel.Debug);

	/// <summary>Возвращает конфигурацию логирования приложения.</summary>
	/// <param name="serviceName">Имя сервиса, для которого получается конфигурация логирования.</param>
	/// <returns>Конфигурация логирования приложения.</returns>
	public static LoggerConfiguration GetBaseLoggerConfig(string serviceName)
	{
		const int logFileSizeLimit = 10 * 1024 * 1024;

		var path = ProfileLocationStorage.LogDirPath;

		return new LoggerConfiguration()
			.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
			.MinimumLevel.Override("System", LogEventLevel.Warning)
			.Enrich.FromLogContext()
			.WriteTo.Logger(lc =>
				lc.Filter.ByIncludingOnly(logEvent => logEvent.Level <= LogEventLevel.Debug)
					.WriteTo.File
					(
						$@"{path}.dbg_.txt",
						rollOnFileSizeLimit: true,
						rollingInterval: RollingInterval.Day,
						fileSizeLimitBytes: logFileSizeLimit
					))
			.WriteTo.Console()
			.WriteTo.Logger(lc =>
				lc.Filter.ByIncludingOnly(logEvent => logEvent.Level == LogEventLevel.Verbose)
					.WriteTo.File
					(
						$@"{path}.trc_.txt",
						rollOnFileSizeLimit: true,
						rollingInterval: RollingInterval.Day,
						fileSizeLimitBytes: logFileSizeLimit
					))
			.WriteTo.File
			(
				$@"{path}.err_.txt",
				rollOnFileSizeLimit: true,
				rollingInterval: RollingInterval.Day,
				fileSizeLimitBytes: logFileSizeLimit,
				restrictedToMinimumLevel: LogEventLevel.Error
			)
			.WriteTo.File
			(
				$@"{path}_.txt",
				rollOnFileSizeLimit: true,
				rollingInterval: RollingInterval.Day,
				fileSizeLimitBytes: logFileSizeLimit,
				restrictedToMinimumLevel: LogEventLevel.Debug
			);
	}
}