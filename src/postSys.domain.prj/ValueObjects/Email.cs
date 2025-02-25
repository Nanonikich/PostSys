using System.Text.RegularExpressions;

using CSharpFunctionalExtensions;

namespace PostSys.Domain.ValueObjects;

/// <summary>Объект значения, описывающий Email.</summary>
public partial class Email : ValueObject
{
	#region Properties

	/// <summary>Возвращает Email.</summary>
	/// <value>Email.</value>
	public string Value { get; }

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="Email"/>.</summary>
	/// <param name="value">Значение Email.</param>
	/// <exception cref="ArgumentException">Исключение при неверном формате значения.</exception>
	private Email(string value)
	{
		Value = value;
	}

	#endregion

	#region Methods

	private static bool IsValidEmail(string email)
	{
		return MyRegex().IsMatch(email);
	}

	/// <summary>Создаёт Email.</summary>
	/// <param name="value">Значение Email.</param>
	/// <returns>Результат задания Email.</returns>
	public static Result<Email> Create(string value)
	{
		if(!IsValidEmail(value))
			return Result.Failure<Email>("Invalid email format.");

		return Result.Success(new Email(value));
	}

	/// <inheritdoc/>
	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return new List<object> { Value };
	}

	[GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
	private static partial Regex MyRegex();

	#endregion
}