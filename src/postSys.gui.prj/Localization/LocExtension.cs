using System;

using Avalonia.Data;
using Avalonia.Markup.Xaml;

namespace PostSys.Gui.Localization;

/// <summary>Расширение разметки для подстановки локализованной строки.</summary>
public sealed class LocExtension : MarkupExtension
{
    /// <summary>Создаёт расширение разметки.</summary>
    public LocExtension() { }

    /// <summary>Создаёт расширение разметки с ключом.</summary>
    /// <param name="key">Ключ ресурса.</param>
    public LocExtension(string key)
    {
        Key = key;
    }

    /// <summary>Ключ строки в ресурсах.</summary>
    public string Key { get; set; } = string.Empty;

    /// <inheritdoc/>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Key);
        return new Binding($"[{Key}]")
        {
            Source = UiLocalizer.Notifier,
            Mode = BindingMode.OneWay
        };
    }
}