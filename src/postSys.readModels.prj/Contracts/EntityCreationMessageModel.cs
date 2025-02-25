using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace PostSys.ReadModels.Contracts;

/// <summary>Модель сообщения, отправляемого подписчикам, о создании сущности.</summary>
[DataContract]
public class EntityCreationMessageModel
{
	#region Properties

	/// <summary>Возвращает сущность, над которой была выполнена операция.</summary>
	/// <value>Сущность, над которой была выполнена операция.</value>
	[JsonPropertyName("entity")]
	public string Entity { get; }

	/// <summary>Возвращает идентификатор сущности.</summary>
	/// <value>Идентификатор сущности.</value>
	[JsonPropertyName("id")]
	public Guid Id { get; }

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="EntityParameterChangeMessageModel"/>.</summary>
	/// <param name="entity">Сущность, над которой была выполнена операция.</param>
	/// <param name="id">Идентификатор сущности.</param>
	public EntityCreationMessageModel(string entity, Guid id)
	{
		Entity = entity;
		Id = id;
	}

	#endregion
}

