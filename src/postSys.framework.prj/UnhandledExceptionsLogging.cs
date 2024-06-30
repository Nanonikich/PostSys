using Serilog;

namespace PostSys.Framework;

/// <summary>Класс для подключения логирования необработанных исключений приложения на основе логгера Serilog.</summary>
public static class UnhandledExceptionsLogging
{
	private static bool _isSubscribed;
	private static readonly UnhandledExceptionEventHandler _handler;

	static UnhandledExceptionsLogging()
	{
		_handler = new UnhandledExceptionEventHandler((_, args) =>
		{
			var ex = (args.ExceptionObject as Exception);
			Log.Logger.Fatal(ex, $"Произошла фатальная ошибка. Подробности: {ex.Message:exceptionMessage}.");
		});
	}

	/// <summary>Подключает логирование необработанных исключений.</summary>
	public static void Subscribe()
	{
		if(!_isSubscribed)
		{
			var currentDomain = AppDomain.CurrentDomain;
			currentDomain.UnhandledException += _handler;
			_isSubscribed = true;
		}
	}

	/// <summary>Отключает логирование необработанных исключений.</summary>
	public static void Unsubscribe()
	{
		if(_isSubscribed)
		{
			var currentDomain = AppDomain.CurrentDomain;
			currentDomain.UnhandledException -= _handler;
			_isSubscribed = false;
		}
	}
}
