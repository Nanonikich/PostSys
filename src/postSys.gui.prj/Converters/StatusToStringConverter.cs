using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using PostSys.Gui.Localization;
using PostSys.ReadModels.Contracts;

namespace PostSys.Gui.Converters;

/// <summary>Преобразует статус посылки в локализованную строку.</summary>
public sealed class StatusToStringConverter : IValueConverter
{
    /// <inheritdoc/>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(value is null)
            return AvaloniaProperty.UnsetValue;

        try
        {
            if(value is PackageStatus status)
            {
                return status switch
                {
                    PackageStatus.OnWarehouse => UiLocalizer.Get("Status_OnWarehouse"),
                    PackageStatus.InWork => UiLocalizer.Get("Status_InWork"),
                    PackageStatus.End => UiLocalizer.Get("Status_End"),
                    _ => UiLocalizer.Get("Status_Unknown")
                };
            }

            return Enum.GetName(value.GetType(), value) ?? UiLocalizer.Get("Status_Unknown");
        }
        catch(Exception ex)
        {
            return new BindingNotification(ex, BindingErrorType.Error);
        }
    }

    /// <inheritdoc/>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => new BindingNotification(new NotSupportedException(), BindingErrorType.Error);
}