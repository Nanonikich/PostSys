namespace PostSys.ReadModels.Helpers;

/// <summary>Объект значения, описывающий адрес.</summary>
public class Address
{
	#region Properties

	/// <summary>Возвращает адрес - долготу.</summary>
	/// <value>Адрес - долгота.</value>
	public int Longitude { get; init; }

	/// <summary>Возвращает адрес - широту.</summary>
	/// <value>Адрес - широта.</value>
	public int Latitude { get; init; }

	#endregion
}