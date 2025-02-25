using System.Windows.Input;

using ReactiveUI;

using PostSys.Gui.Common.ViewModels;

namespace PostSys.Gui.Helpers;

/// <summary>Настройки для основных кнопок.</summary>
public sealed class ButtonConfiguration : ViewModelBase
{
	#region Data

	private string _title;
	private string _imageSource;
	private bool _isEnabled;

	#endregion

	#region Properties

	/// <summary>Возвращает или задаёт заголовок.</summary>
	/// <value>Заголовок.</value>
	public string Title
	{
		get => _title;
		set => this.RaiseAndSetIfChanged(ref _title, value);
	}

	/// <summary>Возвращает или задаёт путь к изображению.</summary>
	/// <value>Путь к изображению.</value>
	public string ImageSource
	{
		get => _imageSource;
		set => this.RaiseAndSetIfChanged(ref _imageSource, value);
	}

	/// <summary>Возвращает или задаёт состояние кнопки.</summary>
	/// <value>Состояние кнопки.</value>
	public bool IsEnabled
	{
		get => _isEnabled;
		set => this.RaiseAndSetIfChanged(ref _isEnabled, value);
	}

	/// <summary>Возвращает команду нажатия кнопки.</summary>
	/// <value>Команда нажатия кнопки.</value>
	public ICommand ClickCommand { get; init; }

	#endregion
}
