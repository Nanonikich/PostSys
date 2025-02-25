using System;
using System.Globalization;

using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using PostSys.ReadModels.Helpers;

namespace PostSys.Gui.Converters;

/// <summary>Конвертер перевода значения имени в string для отображения.</summary>
public sealed class FullnameToStringConverter : IValueConverter
{
	#region Methods

	/// <inheritdoc/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if(value != null && value is Fullname inputValue)
		{
			return $"{inputValue.Surname} {inputValue.Name} {inputValue.Patronymic}";
		}
		else
		{
			return AvaloniaProperty.UnsetValue;
		}
	}

	/// <inheritdoc/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		=> new BindingNotification(new NotSupportedException(), BindingErrorType.Error);

	#endregion
}

