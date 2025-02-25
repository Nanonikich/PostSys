namespace PostSys.Dal.Common.Contracts;

/// <summary>Представляет тип единицы работы.</summary>
public enum UnitOfWorkType
{
	/// <summary>Сессия.</summary>
	Session,

	/// <summary>Транзакция.</summary>
	Transaction
}