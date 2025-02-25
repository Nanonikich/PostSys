using CSharpFunctionalExtensions;

namespace PostSys.Domain.ValueObjects;

/// <summary>Объект значения, описывающий данные об имени.</summary>
public class Fullname : ValueObject
{
	#region Properties

	/// <summary>Возвращает фамилию.</summary>
	/// <value>Фамилия.</value>
	public string Surname { get; }

	/// <summary>Возвращает имя.</summary>
	/// <value>Имя.</value>
	public string Name { get; }

	/// <summary>Возвращает отчество.</summary>
	/// <value>Отчество.</value>
	public string? Patronymic { get; }

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="Fullname"/>.</summary>
	/// <param name="surname">Фамилия.</param>
	/// <param name="name">Имя.</param>
	/// <param name="patronymic">Отчество.</param>
	private Fullname(string surname, string name, string? patronymic)
	{
		if(string.IsNullOrWhiteSpace(surname))
			throw new ArgumentException("Surname is required.", nameof(surname));
		if(string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Name is required.", nameof(name));

		Surname = surname;
		Name = name;
		Patronymic = patronymic;
	}

	#endregion

	#region Methods

	/// <summary>Создаёт данные об имени пользователя.</summary>
	/// <param name="surname">Фамилия.</param>
	/// <param name="name">Имя.</param>
	/// <param name="patronymic">Отчество.</param>
	/// <returns>Результат с данными об имени пользователя.</returns>
	public static Result<Fullname> Create(string surname, string name, string? patronymic)
	{
		return Result.Success(new Fullname(surname, name, patronymic));
	}

	/// <inheritdoc/>
	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Surname;
		yield return Name;
		yield return Patronymic;
	}

	#endregion
}