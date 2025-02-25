using System;

using HotChocolate;

using PostSys.Common.Data;
using PostSys.ReadModels.Helpers;

namespace PostSys.ReadModels;

/// <summary>Модель, описывающая клиента.</summary>
public class Client : ISoftDelete
{
	#region Properties

	/// <summary>Возвращает идентификатор клиента.</summary>
	/// <value>Идентификатор клиента.</value>
	public Guid Id { get; init; }

	/// <summary>Возвращает данные об имени клиента.</summary>
	/// <value>Данные об имени клиента.</value>
	public Fullname Fullname { get; init; } = null!;

	/// <summary>Возвращает номер телефона клиента.</summary>
	/// <value>Номер телефона клиента.</value>
	public string PhoneNumber { get; init; }

	/// <inheritdoc/>
	[GraphQLIgnore]
	public bool IsDeleted { get; init; }

	#endregion
}