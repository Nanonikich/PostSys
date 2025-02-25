using CSharpFunctionalExtensions;

namespace PostSys.Domain.ValueObjects;

/// <summary>Статус посылки.</summary>
public class PackageStatus : ValueObject
{
	#region Properties

	/// <summary>Возвращает статус посылки.</summary>
	/// <value>Статус посылки.</value>
	public Contracts.PackageStatus Value { get; }

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="PackageStatus"/>.</summary>
	/// <param name="value">Значение статуса посылки.</param>
	private PackageStatus(Contracts.PackageStatus value)
	{
		Value = value;
	}

	#endregion

	#region Methods

	/// <summary>Задаёт значение статуса посылки.</summary>
	/// <param name="value">Значение статуса посылки.</param>
	/// <returns>Результат задания значения статуса посылки.</returns>
	public static Result<PackageStatus> Create(Contracts.PackageStatus value)
	{ 
		return Result.Success(new PackageStatus(value));
	}

	/// <inheritdoc/>
	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return new List<object> { Value };
	}

	#endregion
}
