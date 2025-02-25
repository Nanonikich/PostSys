using System.Linq.Expressions;
using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using PostSys.Common.Data;

namespace PostSys.Dal.Extensions;

/// <summary>Предоставляет методы расширения для <see cref="IMutableEntityType"/>.</summary>
public static class MutableEntityTypeExtension
{
	#region Methods

	/// <summary>Добавляет настройки для soft delete.</summary>
	/// <param name="entityType">Тип сущности.</param>
	public static void AddSoftDelete(this IMutableEntityType entityType)
	{
		var queryFilter = typeof(MutableEntityTypeExtension)
			.GetMethod("GetSoftDeleteFilter", BindingFlags.Static | BindingFlags.NonPublic)
			?.MakeGenericMethod(entityType.ClrType)
			.Invoke(null, []);
		entityType.SetQueryFilter(queryFilter as LambdaExpression);
		entityType
			.GetProperty("IsDeleted")
			.SetColumnName("is_deleted");
	}

	private static LambdaExpression GetSoftDeleteFilter<TEntity>() where TEntity : class, ISoftDelete
	{
		return (TEntity x) => !x.IsDeleted;
	}

	#endregion
}