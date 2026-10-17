using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PostSys.Dal.Extensions;

namespace PostSys.Dal.ReadModel;

/// <summary>Определяет контекст для чтения данных.</summary>
public sealed class ReadDbContext : DbContext
{
    /// <summary>Создаёт экземпляр класса <see cref="ReadDbContext" />.</summary>
    /// <param name="options">Настройки контекста.</param>
    public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            type => type.Namespace!.StartsWith(
                typeof(ReadDbContext).Namespace!));
        modelBuilder.AddSoftDeleteQueryFilters();
    }
}