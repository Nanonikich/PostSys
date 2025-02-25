using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace PostSys.Domain.ValueObjects;

/// <summary>Объект значения, описывающий номер телефона.</summary>
public partial class PhoneNumber : ValueObject
{
	#region Constants

	private const string PhoneRegex = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";

	#endregion

	#region Properties

	/// <summary>Возвращает номер телефона.</summary>
	/// <value>Номера телефона.</value>
	public string Value { get; }

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="PhoneNumber"/>.</summary>
	/// <param name="value">Значение номера телефона.</param>
	private PhoneNumber(string value)
	{
		Value = value;
	}

	#endregion

	#region Methods

	/// <summary>Создаёт номер телефона.</summary>
	/// <param name="value">Значение номера телефона.</param>
	/// <returns>Результат задания номера телефона.</returns>
	public static Result<PhoneNumber> Create(string value)
	{
		if(string.IsNullOrWhiteSpace(value))
			return Result.Failure<PhoneNumber>("Phone number cannot be empty.");

		if(MyRegex().IsMatch(value) == false)
			return Result.Failure<PhoneNumber>("Phone number is not valid.");

		return Result.Success(new PhoneNumber(value));
	}

	/// <inheritdoc/>
	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}

	[GeneratedRegex(PhoneRegex)]
	private static partial Regex MyRegex();

	#endregion
}