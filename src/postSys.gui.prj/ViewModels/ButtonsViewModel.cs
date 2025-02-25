using System;
using System.Collections.Generic;
using System.Linq;

using Avalonia.Collections;

using ReactiveUI;

using PostSys.Gui.Common.ViewModels;
using PostSys.Gui.Helpers;

namespace PostSys.Gui.ViewModels;

/// <summary>Модель представления основных кнопок.</summary>
public class ButtonsViewModel : ViewModelBase
{
	#region Data

	private AvaloniaList<ButtonConfiguration> _buttonOptions = [];

	#endregion

	#region Properties

	/// <summary>Возвращает или задаёт настройки для кнопок.</summary>
	/// <value>Настройки для кнопок.</value>
	public AvaloniaList<ButtonConfiguration> ButtonOptions
	{
		get => _buttonOptions;
		set => this.RaiseAndSetIfChanged(ref _buttonOptions, value);
	}

	#endregion

	#region Events

	/// <summary>Событие нажатия основной кнопки.</summary>
	public event EventHandler<string> ButtonClickedEvent;

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="ButtonsViewModel"/>.</summary>
	public ButtonsViewModel() => InitializeButtonOptions();

	#endregion

	#region Methods

	/// <summary>Инициализирует параметры кнопок.</summary>
	private void InitializeButtonOptions()
	{
		ButtonOptions.AddRange(
		[
			CreateButton("Profile", "/Assets/Buttons/profile.png", true),
			CreateButton("Orders", "/Assets/Buttons/package.png", false),
			CreateButton("Current order", "/Assets/Buttons/map.png", false)
		]);
	}

	/// <summary>Создаёт конфигурацию кнопки.</summary>
	/// <param name="title">Название кнопки.</param>
	/// <param name="imageSource">Источник изображения.</param>
	/// <param name="isEnabled">Признак доступности кнопки.</param>
	/// <returns>Конфигурация кнопки.</returns>
	private ButtonConfiguration CreateButton(string title, string imageSource, bool isEnabled) => new()
	{
		Title = title,
		ImageSource = imageSource,
		IsEnabled = isEnabled,
		ClickCommand = ReactiveCommand.Create(() => ButtonClickedEvent?.Invoke(this, title))
	};

	/// <summary>Меняет состояние кнопок.</summary>
	/// <param name="buttonStatuses">Данные для обновления состояния кнопок.</param>
	public void SetButtonEnabled(Dictionary<string, bool> buttonStatuses)
	{
		foreach(var item in buttonStatuses)
		{
			var button = ButtonOptions.FirstOrDefault(x => x.Title == item.Key);
			if(button != null)
			{
				button.IsEnabled = item.Value;
			}
		}
	}

	#endregion
}
