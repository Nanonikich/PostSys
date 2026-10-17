using Microsoft.EntityFrameworkCore;

namespace PostSys.Dal.Common.UnitOfWork;

/// <summary>Определяет интерфейс для доступа к <see cref="DbContext"/> в тестовых наборах.</summary>
internal interface IDbContextTestImpurity
{
	#region Properties

	/// <summary>Возвращает <see cref="DbContext"/>.</summary>
	/// <value><see cref="DbContext"/>.</value>
	DbContext Context { get; }

	#endregion
}