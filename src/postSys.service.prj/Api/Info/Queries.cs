using HotChocolate.Types;

using PostSys.Framework.Common;

namespace PostSys.Service.Api.Info;

/// <summary>Определяет запросы GraphQL для получения информации о сервисе.</summary>
[QueryType]
public static class Queries
{
	#region Methods

	/// <summary>Возвращает клиента по идентификатору.</summary>
	/// <returns>Клиент, если он существует.</returns>
	public static string GetVersion()
	{
		var assembly = AssemblyInfo.FromEntryAssembly();
		return $"{assembly.Version!.Major}.{assembly.Version.Minor}.{assembly.Version.Build}";
	}

	#endregion
}
