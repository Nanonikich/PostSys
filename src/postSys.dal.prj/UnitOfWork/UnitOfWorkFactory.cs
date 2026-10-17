using System;

using Microsoft.EntityFrameworkCore;

using PostSys.Dal.Common.Contracts;

namespace PostSys.Dal.Common.UnitOfWork;

/// <summary>Представляет реализацию <see cref="IUnitOfWorkFactory" /> на основе <see cref="DbContext" />.</summary>
/// <typeparam name="TContext">Тип контекста.</typeparam>
public class UnitOfWorkFactory<TContext> : IUnitOfWorkFactory where TContext : DbContext
{
	#region Data

	private readonly TContext _dbContext;

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="UnitOfWorkFactory{TContext}"/>.</summary>
	/// <param name="dbContext">Контекст базы данных.</param>
	/// <exception cref="ArgumentNullException">Значение <paramref name="dbContext"/> не определено.</exception>
	public UnitOfWorkFactory(TContext dbContext)
	{
		_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
	}

	#endregion

	#region Methods

	/// <summary>Создает единицу работы с БД.</summary>
	/// <param name="unitOfWorkType">Тип единицы работы.</param>
	/// <returns>Единица работы.</returns>
	public IUnitOfWork Create(UnitOfWorkType unitOfWorkType = UnitOfWorkType.Transaction) =>
		new UnitOfWork<TContext>(_dbContext);

	#endregion
}