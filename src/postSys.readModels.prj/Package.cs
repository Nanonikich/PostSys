using System;

using HotChocolate;

using PostSys.Common.Data;
using PostSys.ReadModels.Contracts;
using PostSys.ReadModels.Helpers;

namespace PostSys.ReadModels;

/// <summary>Модель, описывающая посылку.</summary>
public class Package : ISoftDelete
{
	#region Properties

	/// <summary>Возвращает идентификатор посылки.</summary>
	/// <value>Идентификатор посылки.</value>
	public Guid Id { get; init; }

	/// <summary>Возвращает параметры посылки.</summary>
	/// <value>Параметры посылки.</value>
	public Dimensions Dimensions { get; init; } = null!;

	/// <summary>Возвращает адрес посылки.</summary>
	/// <value>Адрес посылки.</value>
	public Address Address { get; init; } = null!;

	/// <summary>Возвращает статус посылки.</summary>
	/// <value>Статус посылки.</value>
	public PackageStatus Status { get; init; }

	/// <summary>Возвращает идентификатор клиента, отправившего посылку.</summary>
	/// <value>Идентификатор клиента, отправившего посылку.</value>
	public Guid PackageClientId { get; init; }

	/// <summary>Возвращает идентификатор почтальона, доставляющего посылку.</summary>
	/// <value>Идентификатор почтальона, доставляющего посылку.</value>
	public Guid PackagePostmanId { get; init; }

	/// <inheritdoc/>
	[GraphQLIgnore]
	public bool IsDeleted { get; init; }

	#endregion
}