using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace PostSys.ReadModels.Contracts;

/// <summary>Модель сообщения, отправляемого подписчикам, об изменении параметра сущности.</summary>
[DataContract]
public class EntityParameterChangeMessageModel
{
	#region Properties

	/// <summary>Возвращает сущность, над которой была выполнена операция.</summary>
	/// <value>Сущность, над которой была выполнена операция.</value>
	[JsonPropertyName("entity")]
	public string Entity { get; }

	/// <summary>Возвращает действие, выполненное над сущностью.</summary>
	/// <value>Действие, выполненное над сущностью.</value>
	[JsonPropertyName("action")]
	public string Action { get; }

	/// <summary>Возвращает идентификатор сущности.</summary>
	/// <value>Идентификатор сущности.</value>
	[JsonPropertyName("id")]
	public Guid Id { get; }

	/// <summary>Возвращает новое значение параметра сущности.</summary>
	/// <value>Новое значение параметра сущности.</value>
	[JsonPropertyName("value")]
	public string Value { get; }

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="EntityParameterChangeMessageModel"/>.</summary>
	/// <param name="entity">Сущность, над которой была выполнена операция.</param>
	/// <param name="action">Действие, выполненное над сущностью.</param>
	/// <param name="id">Идентификатор сущности.</param>
	/// <param name="value">Новое значение параметра сущности.</param>
	public EntityParameterChangeMessageModel(string entity, string action, Guid id, string value)
	{
		Entity = entity;
		Action = action;
		Id = id;
		Value = value;
	}

	#endregion
}
