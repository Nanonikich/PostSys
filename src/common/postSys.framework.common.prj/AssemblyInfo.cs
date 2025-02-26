using System;
using System.Reflection;

namespace PostSys.Framework.Common;

/// <summary>Информация о сборке.</summary>
/// <param name="assembly">Сборка, по которой собирается информация.</param>
public class AssemblyInfo(Assembly assembly)
{
	/// <summary>Возвращает информацию о сборке, содержащей точку входа.</summary>
	/// <value>Информация о сборке, содержащей точку входа.</value>
	public static AssemblyInfo EntryAssembly { get; } = FromEntryAssembly();

	/// <summary>Возвращает сборку, по которой собирается информация.</summary>
	/// <value>Сборка, по которой собирается информация.</value>
	public Assembly Assembly { get; } = assembly;

	/// <summary>Возвращает версию сборки.</summary>
	/// <value>Версия сборки.</value>
	public Version? Version => Assembly.GetName().Version;

	/// <summary>Возвращает информацию о сборке, из которой вызывается данный метод.</summary>
	/// <returns>Информацию о сборке, из которой вызывается данный метод.</returns>
	public static AssemblyInfo FromCallingAssembly()
	{
		return new AssemblyInfo(Assembly.GetCallingAssembly());
	}

	/// <summary>Возвращает информацию о сборке, содержащей точку входа.</summary>
	/// <returns>Информация о сборке.</returns>
	public static AssemblyInfo FromEntryAssembly()
	{
		return new AssemblyInfo(Assembly.GetEntryAssembly() ?? throw new InvalidOperationException("Entry assembly is not defined."));
	}
}
