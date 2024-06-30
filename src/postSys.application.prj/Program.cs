using Autofac;

using Serilog;

using PostSys.Application.Framework;
using PostSys.Application.Views.Forms;

namespace PostSys.Application;

public static class Program
{
	public static ILogger Log { get; private set; } 

	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	static void Main()
	{
		ProfileLocationStorage.ProfileRootDir = PathConstants.AppProgramDataPath;

		Serilog.Log.Logger = SwitchableLogger.Instance.Logger = SwitchableLogger.Instance;
		SwitchableLogger.Instance.Logger = GetBaseLoggerConfig().CreateLogger();
		Log = Serilog.Log.ForContext(typeof(Program));

		ApplicationConfiguration.Initialize();

		var bootStrapper = new BootStrapper();
		var container = bootStrapper.BootStrap;
		var loginForm = container.Resolve<AuthentificationForm>();

		System.Windows.Forms.Application.Run(loginForm);
	}

	/// <summary>Возвращает конфигурацию логирования приложения.</summary>
	/// <returns>Конфигурация логирования приложения.</returns>
	internal static LoggerConfiguration GetBaseLoggerConfig()
		=> BaseLoggerConfiguarationProvider.GetBaseLoggerConfig("postSys.application");
}