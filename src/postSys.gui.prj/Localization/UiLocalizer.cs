using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace PostSys.Gui.Localization;

/// <summary>Доступ к строкам интерфейса из файлов ресурсов.</summary>
public static class UiLocalizer
{
    private static readonly ResourceManager Resources =
        new("PostSys.Gui.Localization.UiStrings", typeof(UiLocalizer).Assembly);

    /// <summary>Код культуры по умолчанию.</summary>
    public const string DefaultCultureName = "ru-RU";

    /// <summary>Объект для привязки разметки к смене языка.</summary>
    public static UiLocalizationNotifier Notifier { get; } = new();

    /// <summary>Возвращает строку по ключу для текущей культуры интерфейса.</summary>
    /// <param name="key">Ключ ресурса.</param>
    /// <returns>Локализованная строка или сам ключ, если перевод не найден.</returns>
    public static string Get(string key)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        return Resources.GetString(key, CultureInfo.CurrentUICulture) ?? key;
    }

    /// <summary>Возвращает строку с подстановкой аргументов.</summary>
    /// <param name="key">Ключ ресурса.</param>
    /// <param name="args">Аргументы форматирования.</param>
    /// <returns>Отформатированная локализованная строка.</returns>
    public static string Format(string key, params object[] args)
        => string.Format(CultureInfo.CurrentUICulture, Get(key), args);

    /// <summary>Применяет культуру интерфейса ко всему приложению.</summary>
    /// <param name="cultureName">Имя культуры (например, ru-RU или en).</param>
    public static void ApplyCulture(string? cultureName)
    {
        var name = string.IsNullOrWhiteSpace(cultureName) ? DefaultCultureName : cultureName.Trim();
        CultureInfo culture;
        try
        {
            culture = CultureInfo.GetCultureInfo(name);
        }
        catch(CultureNotFoundException)
        {
            culture = CultureInfo.GetCultureInfo(DefaultCultureName);
        }

        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
        Notifier.RaiseCultureChanged();
    }
}

/// <summary>Уведомитель смены культуры для привязок разметки.</summary>
public sealed class UiLocalizationNotifier : INotifyPropertyChanged
{
    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>Индексатор строк по ключу ресурса.</summary>
    /// <param name="key">Ключ ресурса.</param>
    public string this[string key] => UiLocalizer.Get(key);

    /// <summary>Сообщает об изменении культуры интерфейса.</summary>
    public void RaiseCultureChanged()
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item[]"));
}