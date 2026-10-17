using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace PostSys.IntegrationTests.Common.Docker.Database;

/// <summary>Представляет исполнителя для миграций.</summary>
/// <param name="appServiceProvider">Провайдер зарегистрированных сервисов.</param>
/// <typeparam name="TDbContext">Тип контекста БД.</typeparam>
public class MigrationPerformer<TDbContext>(IServiceProvider appServiceProvider) where TDbContext : DbContext
{
	#region Methods

	/// <summary>Отменяет все миграции.</summary>
	/// <returns>Асинхронная операция.</returns>
	public async Task MigrateDownAsync()
	{
		await ExecuteWithDbContextAsync(async dbContext => await dbContext.GetService<IMigrator>().MigrateAsync("0"));
	}

	/// <summary>Поднимает миграции до последнего актуального значения.</summary>
	/// <returns>Асинхронная операция.</returns>
	public async Task MigrateUpAsync()
	{
		await ExecuteWithDbContextAsync(async dbContext => await dbContext.Database.MigrateAsync());
	}

	private async Task ExecuteWithDbContextAsync(Func<TDbContext, Task> action)
	{
		await using var serviceScope = appServiceProvider.CreateAsyncScope();
		var dbContext = serviceScope.ServiceProvider.GetRequiredService<TDbContext>();

		await action(dbContext);
	}

	#endregion
}