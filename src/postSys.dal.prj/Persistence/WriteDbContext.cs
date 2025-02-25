using System.Reflection;
using Microsoft.EntityFrameworkCore;

using PostSys.Dal.Extensions;

namespace PostSys.Dal.Persistence;

/// <summary>Определяет контекст для записи данных.</summary>
/// <param name="options">Настройки контекста.</param>
internal sealed class WriteDbContext(DbContextOptions<WriteDbContext> options) : DbContext(options)
{
	#region Methods

	/// <inheritdoc/>
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(
			Assembly.GetExecutingAssembly(),
			type => type.Namespace!.StartsWith(typeof(WriteDbContext).Namespace!));
		modelBuilder.AddSoftDeleteQueryFilters();
	}

	#endregion
}