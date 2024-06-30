using Microsoft.EntityFrameworkCore;

namespace PostSys.Application.Context;

/// <summary>Конфигурация контекста базы данных.</summary>
public class DbContextConfigurer
{
	/// <summary>Настраивает контекст базы данных.</summary>
	/// <param name="builder">Конфигуратор настроек базы данных.</param>
	/// <param name="connectionString">Строка подключения.</param>
	public static void Configure(DbContextOptionsBuilder<PostSysContext> builder, string connectionString)
	{
		builder.UseSqlServer(connectionString);
	}
}
