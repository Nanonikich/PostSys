using System;

namespace PostSys.Service.Common.Exceptions;

/// <summary>
/// Представляет исключение, возникающее в случае, если заданная сущность не была найдена.
/// </summary>
public sealed class EntityNotFoundException : Exception
{
	#region Properties

	/// <summary>Возвращает текстовое представление идентификатора сущности.</summary>
	/// <value>Текстовое представление идентификатора сущности.</value>
	public string EntityId { get; }

	/// <summary>Возвращает тип сущности.</summary>
	/// <value>Тип сущности.</value>
	public string EntityType { get; }

	#endregion

	#region .ctor

	/// <summary>Конструктор по умолчанию.</summary>
	public EntityNotFoundException()
		: base("Entity not found.")
	{
	}

	/// <summary>Создаёт экземпляр класса <see cref="EntityNotFoundException" />.</summary>
	/// <param name="entityType">Тип сущности.</param>
	/// <param name="entityId">Текстовое представление идентификатора сущности.</param>
	public EntityNotFoundException(string entityType, string entityId)
		: base(
			!string.IsNullOrWhiteSpace(entityId) || !string.IsNullOrWhiteSpace(entityType)
			? string.IsNullOrWhiteSpace(entityType) ? "Entity not found." 
			: string.IsNullOrWhiteSpace(entityId) ? "Entity with type not found." 
			: "Entity with type and id not found." : "Entity not found.")
	{
		EntityType = entityType;
		EntityId = entityId;
	}

	#endregion

	#region Methods

	/// <summary>Проверяет сущность на Null и если  равна, создает исключение.</summary>
	/// <param name="entity">Проверяемая сущность.</param>
	/// <param name="entityType">Тип сущности.</param>
	/// <param name="entityId">Текстовое представление идентификатора сущности.</param>
	/// <exception cref="EntityNotFoundException">Если сущность равна Null.</exception>
	public static void ThrowIfNull(object? entity, string entityType, string entityId)
	{
		if(entity == null) throw new EntityNotFoundException(entityType, entityId);
	}

	#endregion
}