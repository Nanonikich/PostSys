using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using PostSys.Common.Data;

namespace PostSys.Dal.Extensions;

/// <summary>Предоставляет методы расширения для <see cref="ModelBuilder"/>.</summary>
public static class ModelBuilderExtensions
{
	#region Methods

	/// <summary>Добавляет фильтры для soft delete.</summary>
	/// <param name="modelBuilder"><see cref="ModelBuilder"/>.</param>
	public static void AddSoftDeleteQueryFilters(this ModelBuilder modelBuilder)
	{
		ArgumentNullException.ThrowIfNull(modelBuilder, nameof(modelBuilder));
		foreach(var entityType in modelBuilder.Model
			.GetEntityTypes()
			.Where(type => typeof(ISoftDelete).IsAssignableFrom(type.ClrType)))
		{
			entityType.AddSoftDelete();
		}
	}

	#endregion
}