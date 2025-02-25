using PostSys.Dal.Common.Contracts;

namespace PostSys.Dal.Common.UnitOfWork;

/// <summary>Представляет интерфейс фабрики единицы работы.</summary>
public interface IUnitOfWorkFactory
{
	#region Methods

	/// <summary>Создает единицу работы с БД.</summary>
	/// <param name="unitOfWorkType">Тип единицы работы.</param>
	/// <returns>Единица работы.</returns>
	IUnitOfWork Create(UnitOfWorkType unitOfWorkType = UnitOfWorkType.Session);

	#endregion
}