using System;
using System.Collections.Generic;
using System.Linq;

using Avalonia.Collections;

using ReactiveUI;

using PostSys.Gui.Common.ViewModels;
using PostSys.Gui.Helpers;
using PostSys.Gui.Localization;

namespace PostSys.Gui.ViewModels;

/// <summary>Модель представления основных кнопок навигации.</summary>
public class ButtonsViewModel : ViewModelBase
{
	private AvaloniaList<ButtonConfiguration> _buttonOptions = [];

	/// <summary>Список настроек кнопок.</summary>
	public AvaloniaList<ButtonConfiguration> ButtonOptions
	{
		get => _buttonOptions;
		set => this.RaiseAndSetIfChanged(ref _buttonOptions, value);
	}

	/// <summary>Событие нажатия кнопки; аргумент — логический ключ кнопки.</summary>
	public event EventHandler<string>? ButtonClickedEvent;

	/// <summary>Создаёт экземпляр класса <see cref="ButtonsViewModel"/>.</summary>
	public ButtonsViewModel() => InitializeButtonOptions();

	/// <summary>Обновляет подписи кнопок после смены языка.</summary>
	public void RefreshTitles()
	{
		foreach(var button in ButtonOptions)
			button.Title = UiLocalizer.Get("Nav_" + button.Key);
	}

	/// <summary>Меняет доступность кнопок по логическим ключам.</summary>
	/// <param name="buttonStatuses">Словарь «ключ кнопки ? доступность».</param>
	public void SetButtonEnabled(Dictionary<string, bool> buttonStatuses)
	{
		foreach(var item in buttonStatuses)
		{
			var button = ButtonOptions.FirstOrDefault(x => x.Key == item.Key);
			if(button != null)
				button.IsEnabled = item.Value;
		}
	}

	private void InitializeButtonOptions()
	{
		ButtonOptions.AddRange(
		[
			CreateButton("Profile", "/Assets/Buttons/profile.png", true),
			CreateButton("Orders", "/Assets/Buttons/package.png", false),
			CreateButton("CurrentOrder", "/Assets/Buttons/map.png", false)
		]);
	}

	private ButtonConfiguration CreateButton(string key, string imageSource, bool isEnabled)
	{
		return new ButtonConfiguration
		{
			Key = key,
			Title = UiLocalizer.Get("Nav_" + key),
			ImageSource = imageSource,
			IsEnabled = isEnabled,
			ClickCommand = ReactiveCommand.Create(() => ButtonClickedEvent?.Invoke(this, key))
		};
	}
}
