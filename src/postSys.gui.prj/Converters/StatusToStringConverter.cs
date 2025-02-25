using System;
using System.Globalization;

using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;

using PostSys.ReadModels.Contracts;

namespace PostSys.Gui.Converters;

/// <summary>Конвертер перевода значения enum в string для отображения.</summary>
public sealed class StatusToStringConverter : IValueConverter
{
	#region Methods

	/// <inheritdoc/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if(value != null)
		{
			try
			{
				var enumType = value.GetType();
				var enumName = Enum.GetName(enumType, value);

				if(enumName != null && Enum.TryParse(enumType, enumName, out var enumValue))
				{
					if(enumValue is PackageStatus status)
					{
						return status switch
						{
							PackageStatus.OnWarehouse => "On Warehouse",
							PackageStatus.InWork => "In Work",
							PackageStatus.End => "End",
							_ => "Unknown"
						};
					}
				}

				return enumName ?? "Неизвестный статус";
			}
			catch(Exception ex)
			{
				return new BindingNotification(ex, BindingErrorType.Error);
			}
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

