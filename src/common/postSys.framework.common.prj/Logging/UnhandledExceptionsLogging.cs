using System;
using System.Threading.Tasks;

using NLog;

namespace PostSys.Framework.Common.Logging;

/// <summary>Класс для подключения логирования необработанных исключений приложения на основе логгера NLog.</summary>
public static class UnhandledExceptionsLogging
{
	#region Static

	private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
	private static bool _isSubscribed;

	#endregion

	#region Methods

	/// <summary>Подключает логирование необработанных исключений (идемпотентно).</summary>
	public static void Subscribe()
	{
		if(_isSubscribed)
			return;

		AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
		TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
		_isSubscribed = true;
	}

	/// <summary>Отключает логирование необработанных исключений.</summary>
	public static void Unsubscribe()
	{
		if(!_isSubscribed)
			return;

		AppDomain.CurrentDomain.UnhandledException -= OnUnhandledException;
		TaskScheduler.UnobservedTaskException -= OnUnobservedTaskException;
		_isSubscribed = false;
	}

	private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
	{
		var ex = (Exception)args.ExceptionObject;
		Logger.Fatal(ex, "A fatal error has occurred. Details: {Message}.", ex.Message);
	}

	private static void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs args)
	{
		Logger.Error(args.Exception, "Unobserved task exception. Details: {Message}.", args.Exception.Message);
		args.SetObserved();
	}

	#endregion
}
