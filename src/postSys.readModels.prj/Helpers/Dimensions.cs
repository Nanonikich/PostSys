namespace PostSys.ReadModels.Helpers;

/// <summary>Объект значения, описывающий параметры посылки.</summary>
public class Dimensions
{
	#region Properties

	/// <summary>Возвращает ширину посылки.</summary>
	/// <value>Ширина посылки.</value>
	public double Length { get; init; }

	/// <summary>Возвращает высоту посылки.</summary>
	/// <value>Высота посылки.</value>
	public double Height { get; init; }

	/// <summary>Возвращает вес посылки.</summary>
	/// <value>Вес посылки.</value>
	public double Weight { get; init; }

	#endregion
}