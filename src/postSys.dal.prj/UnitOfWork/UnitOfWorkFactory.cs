using System;
using Microsoft.EntityFrameworkCore;
using PostSys.Application.Abstractions;
using PostSys.Dal.Common.Contracts;

namespace PostSys.Dal.Common.UnitOfWork;

/// <summary>Представляет реализацию <see cref="IUnitOfWorkFactory" /> на основе <see cref="DbContext" />.</summary>
/// <typeparam name="TContext">Тип контекста.</typeparam>
public class UnitOfWorkFactory<TContext> : IUnitOfWorkFactory where TContext : DbContext
{
	private readonly TContext _dbContext;
	private readonly IDomainEventDispatcher _domainEventDispatcher;

	/// <summary>Создаёт экземпляр класса <see cref="UnitOfWorkFactory{TContext}"/>.</summary>
	/// <param name="dbContext">Контекст базы данных.</param>
	/// <param name="domainEventDispatcher">Диспетчер доменных событий.</param>
	/// <exception cref="ArgumentNullException">Значение аргумента не определено.</exception>
	public UnitOfWorkFactory(TContext dbContext, IDomainEventDispatcher domainEventDispatcher)
	{
		_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		_domainEventDispatcher = domainEventDispatcher ?? throw new ArgumentNullException(nameof(domainEventDispatcher));
	}

	/// <summary>Создает единицу работы с БД.</summary>
	/// <param name="unitOfWorkType">Тип единицы работы.</param>
	/// <returns>Единица работы.</returns>
	public IUnitOfWork Create(UnitOfWorkType unitOfWorkType = UnitOfWorkType.Transaction) =>
		new UnitOfWork<TContext>(_dbContext, _domainEventDispatcher);
}