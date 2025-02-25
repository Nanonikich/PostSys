namespace PostSys.Common.Data;

/// <summary>Представляет инструмент, для поддержания soft-удаления.</summary>
public interface ISoftDelete
{
	#region Properties

	/// <summary>Возвращает значение, указывающее, удалена ли сущность.</summary>
	/// <value>Значение, указывающее, удалена ли сущность.</value>
	bool IsDeleted { get; }

	#endregion
}