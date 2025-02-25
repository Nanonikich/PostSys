using System;

namespace PostSys.Service.Common.Converters;

/// <summary>Конвертер одинаковых enum.</summary>
/// <typeparam name="TInput">Тип входного enum.</typeparam>
/// <typeparam name="TOutput">Тип возвращаемого enum.</typeparam>
public static class EnumConverter<TInput, TOutput>
{
	#region Methods

	/// <summary>Конвертирует значение одного enum в значение другого enum.</summary>
	/// <param name="inputEnum">Enum для преобразования.</param>
	/// <returns>Enum заданного типа.</returns>
	public static TOutput Convert(TInput inputEnum)
	{
		return (TOutput)Enum.Parse(typeof(TInput), inputEnum.ToString());
	}

	#endregion
}