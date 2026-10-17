using System.Reflection;
using Microsoft.EntityFrameworkCore;

using PostSys.Dal.Extensions;
using PostSys.Dal.Outbox;

namespace PostSys.Dal.Persistence;

/// <summary>Определяет контекст для записи данных.</summary>
/// <param name="options">Настройки контекста.</param>
internal sealed class WriteDbContext(DbContextOptions<WriteDbContext> options) : DbContext(options)
{
	/// <summary>Transactional outbox.</summary>
	public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

	/// <inheritdoc/>
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(
			Assembly.GetExecutingAssembly(),
			type => type.Namespace!.StartsWith(typeof(WriteDbContext).Namespace!)
				|| type.Namespace == typeof(OutboxMessage).Namespace);
		modelBuilder.AddSoftDeleteQueryFilters();
	}
}