using CSharpFunctionalExtensions;

namespace PostSys.Domain.ValueObjects;

/// <summary>Объект значения, описывающий адрес.</summary>
public class Address : ValueObject
{
	#region Properties

	/// <summary>Возвращает адрес - долготу.</summary>
	/// <value>Адрес - долгота.</value>
	public int Longitude { get; }

	/// <summary>Возвращает адрес - широту.</summary>
	/// <value>Адрес - широта.</value>
	public int Latitude { get; }

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="Address"/>.</summary>
	/// <param name="longitude">Адрес - долгота.</param>
	/// <param name="latitude">Адрес - широта.</param>
	private Address(int longitude, int latitude)
	{
		Longitude = longitude;
		Latitude = latitude;
	}

	#endregion

	#region Methods

	/// <summary>Задаёт адрес.</summary>
	/// <param name="longitude">Адрес - долгота.</param>
	/// <param name="latitude">Адрес - широта.</param>
	/// <returns>Результат задания адреса.</returns>
	public static Result<Address> Create(int longitude, int latitude)
	{
		return Result.Success(new Address(longitude, latitude));
	}

	/// <inheritdoc/>
	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Longitude;
		yield return Latitude;
	}

	#endregion
}