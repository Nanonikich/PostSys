using CSharpFunctionalExtensions;

namespace PostSys.Domain.ValueObjects;

/// <summary>Объект значения, описывающий параметры посылки.</summary>
public class Dimensions : ValueObject
{
	#region Properties

	/// <summary>Возвращает ширину посылки.</summary>
	/// <value>Ширина посылки.</value>
	public double Length { get; }

	/// <summary>Возвращает высоту посылки.</summary>
	/// <value>Высота посылки.</value>
	public double Height { get; }

	/// <summary>Возвращает вес посылки.</summary>
	/// <value>Вес посылки.</value>
	public double Weight { get; }

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="Dimensions"/>.</summary>
	/// <param name="length">Ширина посылки.</param>
	/// <param name="height">Высота посылки.</param>
	/// <param name="weight">Вес посылки.</param>
	private Dimensions(double length, double height, double weight)
	{
		Length = length;
		Height = height;
		Weight = weight;
	}

	#endregion

	#region Methods

	/// <summary>Задаёт параметры посылки.</summary>
	/// <param name="length">Ширина посылки.</param>
	/// <param name="height">Высота посылки.</param>
	/// <param name="weight">Вес посылки.</param>
	/// <returns>Результат задания параметров посылки.</returns>
	public static Result<Dimensions> Create(double length, double height, double weight)
	{
		if(length < 0 || weight < 0 || height < 0)
			return Result.Failure<Dimensions>("Dimensions cannot be negative.");

		return Result.Success(new Dimensions(length, height, weight));
	}

	/// <inheritdoc/>
	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Length;
		yield return Weight;
		yield return Height;
	}

	#endregion
}