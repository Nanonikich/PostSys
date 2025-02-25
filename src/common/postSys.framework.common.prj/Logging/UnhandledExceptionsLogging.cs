using System;

using NLog;

namespace PostSys.Framework.Common.Logging;

/// <summary>Класс для подключения логирования необработанных исключений приложения на основе логгера NLog.</summary>
public static class UnhandledExceptionsLogging
{
	#region Static

	private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
	private static bool _isSubscribed;

	#endregion

	#region .ctor

	static UnhandledExceptionsLogging()
	{
		AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
		{
			var ex = (Exception)args.ExceptionObject;
			Logger.Fatal(ex, "A fatal error has occurred. Details: {Message}.", ex.Message);
		};
	}

	#endregion

	#region Methods

	/// <summary>Подключает логирование необработанных исключений.</summary>
	public static void Subscribe()
	{
		if(!_isSubscribed)
		{
			_isSubscribed = true;
		}
	}

	/// <summary>Отключает логирование необработанных исключений.</summary>
	public static void Unsubscribe()
	{
		if(_isSubscribed)
		{
			_isSubscribed = false;
		}
	}

	#endregion
}