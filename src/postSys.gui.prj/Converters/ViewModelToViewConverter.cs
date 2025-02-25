using System;
using System.Globalization;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace PostSys.Gui.Converters;

/// <summary>Конвертер модели представления к представлению.</summary>
public class ViewModelToViewConverter : IValueConverter
{
	#region Methods

	/// <inheritdoc/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if(value == null)
			return AvaloniaProperty.UnsetValue;

		var viewName = value.GetType().Name.Replace("Model", "");
		var viewType = Type.GetType($"PostSys.Gui.Views.Controls.{viewName}");

		if(viewType == null)
			throw new InvalidOperationException($"View type '{viewName}' not found for ViewModel '{value.GetType().Name}'.");

		var userControl = (UserControl?)Activator.CreateInstance(viewType);
		if(userControl != null)
		{
			userControl.DataContext = value;
		}

		return userControl;
	}

	/// <inheritdoc/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		=> throw new NotImplementedException("ConvertBack is not implemented.");

	#endregion
}