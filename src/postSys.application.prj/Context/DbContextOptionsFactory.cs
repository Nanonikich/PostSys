using System.Configuration;

using Microsoft.EntityFrameworkCore;

namespace PostSys.Application.Context;

/// <summary>Фабрика настроек контекста базы данных.</summary>
public class DbContextOptionsFactory
{
	/// <summary>Получает настройки контекста базы данных.</summary>
	/// <returns>Настройки контекста базы данных.</returns>
	public static DbContextOptions<PostSysContext> Get()
	{
		var builder = new DbContextOptionsBuilder<PostSysContext>();
		DbContextConfigurer.Configure(builder, ConfigurationManager.ConnectionStrings["MySql"].ConnectionString);

		return builder.Options;
	}
}
