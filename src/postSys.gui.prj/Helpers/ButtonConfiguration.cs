using System.Windows.Input;

using ReactiveUI;

using PostSys.Gui.Common.ViewModels;

namespace PostSys.Gui.Helpers;

/// <summary>Настройки основной кнопки навигации.</summary>
public sealed class ButtonConfiguration : ViewModelBase
{
    private string _title = string.Empty;
    private string _imageSource = string.Empty;
    private bool _isEnabled;

    /// <summary>Логический ключ кнопки (не зависит от языка интерфейса).</summary>
    public string Key { get; init; } = string.Empty;

    /// <summary>Отображаемый заголовок кнопки.</summary>
    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    /// <summary>Путь к изображению кнопки.</summary>
    public string ImageSource
    {
        get => _imageSource;
        set => this.RaiseAndSetIfChanged(ref _imageSource, value);
    }

    /// <summary>Признак доступности кнопки.</summary>
    public bool IsEnabled
    {
        get => _isEnabled;
        set => this.RaiseAndSetIfChanged(ref _isEnabled, value);
    }

    /// <summary>Команда нажатия на кнопку.</summary>
    public ICommand ClickCommand { get; init; } = null!;
}