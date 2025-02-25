namespace PostSys.ReadModels.Helpers;

/// <summary>Объект значения, описывающий данные об имени.</summary>
public class Fullname
{
	#region Properties

	/// <summary>Возвращает фамилию.</summary>
	/// <value>Фамилия.</value>
	public string Surname { get; init; } = null!;

	/// <summary>Возвращает имя.</summary>
	/// <value>Имя.</value>
	public string Name { get; init; } = null!;

	/// <summary>Возвращает отчество.</summary>
	/// <value>Отчество.</value>
	public string? Patronymic { get; init; }

	#endregion
}