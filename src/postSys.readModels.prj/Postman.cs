using System;

using HotChocolate;

using PostSys.Common.Data;
using PostSys.ReadModels.Helpers;

namespace PostSys.ReadModels;

/// <summary>Модель, описывающая почтальона.</summary>
public class Postman : ISoftDelete
{
	#region Properties

	/// <summary>Возвращает идентификатор почтальона.</summary>
	/// <value>Идентификатор почтальона.</value>
	public Guid Id { get; init; }

	/// <summary>Возвращает данные об имени почтальона.</summary>
	/// <value>Данные об имени почтальона.</value>
	public Fullname Fullname { get; init; } = null!;

	/// <summary>Возвращает идентификатор посылки.</summary>
	/// <value>Идентификатор посылки.</value>
	public Guid PostmanPackageId { get; init; }

	/// <summary>Возвращает email почтальона для доступа в систему.</summary>
	/// <value>Email почтальона для доступа в систему.</value>
	public string Email { get; init; } = null!;

	/// <summary>Возвращает пароль почтальона для доступа в систему.</summary>
	/// <value>Пароль почтальона для доступа в систему.</value>
	public string Password { get; init; } = null!;

	/// <inheritdoc/>
	[GraphQLIgnore]
	public bool IsDeleted { get; init; }

	#endregion
}