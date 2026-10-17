using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace PostSys.Gui.Common.Converters;

/// <summary>Конвертер изображений в Bitmap.</summary>
public class ImagePathToBitmapConverter : IValueConverter
{
    /// <inheritdoc/>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        try
        {
            var assemblyName = Assembly.GetEntryAssembly()?.GetName().Name
                ?? throw new InvalidOperationException("Entry assembly is not available.");
            return new Bitmap(AssetLoader.Open(new Uri($"avares://{assemblyName}{value}")));
        }
        catch(FileNotFoundException ex)
        {
            return new BindingNotification(ex, BindingErrorType.Error);
        }
        catch(NotSupportedException ex)
        {
            return new BindingNotification(ex, BindingErrorType.Error);
        }
    }

    /// <inheritdoc/>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => new BindingNotification(value);
}