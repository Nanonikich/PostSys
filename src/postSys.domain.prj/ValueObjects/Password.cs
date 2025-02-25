using CSharpFunctionalExtensions;

namespace PostSys.Domain.ValueObjects;

/// <summary>Объект значения, описывающий пароль.</summary>
public class Password : ValueObject
{
	#region Properties

	/// <summary>Возвращает пароль.</summary>
	/// <value>Пароль.</value>
	public string Value { get; }

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="Password"/>.</summary>
	/// <param name="value">Значение пароля.</param>
	private Password(string value)
	{
		Value = value;
	}

	#endregion

	#region Methods

	/// <summary>Создаёт пароль.</summary>
	/// <param name="value">Значение пароля.</param>
	/// <returns>Результат задания пароля.</returns>
	public static Result<Password> Create(string value)
	{
		if(value.Length < 6)
			return Result.Failure<Password>("Password must be at least 6 characters long.");

		return Result.Success(new Password(value));
	}

	/// <inheritdoc/>
	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return new List<object> { Value };
	}

	#endregion
}