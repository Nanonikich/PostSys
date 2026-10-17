using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using MsBox.Avalonia.Enums;
using PostSys.Gui.Common.Helpers;
using PostSys.Gui.Common.Services.IconProvider;
using PostSys.Gui.Common.ViewModels;

namespace PostSys.Gui.Common.Services.Dialogs;

/// <summary>Обработчик показа сообщений и закрытия наложений на мобильном клиенте.</summary>
public class DialogReactiveWindow
{
	private readonly TopLevel _topLevel;
	private readonly Action? _clearOverlay;

	/// <summary>Создаёт обработчик диалогов.</summary>
	/// <param name="topLevel">Корневой визуальный элемент приложения.</param>
	/// <param name="clearOverlay">Скрытие наложения (необязательно).</param>
	public DialogReactiveWindow(TopLevel topLevel, Action? clearOverlay = null)
	{
		_topLevel = topLevel ?? throw new ArgumentNullException(nameof(topLevel));
		_clearOverlay = clearOverlay;
	}

	/// <summary>Поставщик значка приложения.</summary>
	public IApplicationIconProvider IconProvider { get; set; } = null!;

	/// <summary>Показывает сообщение с описанием ошибки.</summary>
	/// <param name="interaction">Контекст взаимодействия с текстом сообщения.</param>
	/// <returns>Задача показа диалога.</returns>
	public async Task ShowErrorMessageAsync(IInteractionContext<MessageContent, bool> interaction)
	{
		var box = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(
			new MsBox.Avalonia.Dto.MessageBoxStandardParams
			{
				ButtonDefinitions = ButtonEnum.Ok,
				Icon = Icon.Error,
				ContentTitle = interaction.Input.Title,
				ContentMessage = interaction.Input.Message,
				WindowStartupLocation = WindowStartupLocation.CenterScreen,
				WindowIcon = IconProvider.Icon,
			});

		var result = _topLevel is Window window
			? await box.ShowWindowDialogAsync(window)
			: await box.ShowAsPopupAsync(_topLevel);

		interaction.SetOutput(result == ButtonResult.Ok);
	}

	/// <summary>Показывает сообщение с вопросом.</summary>
	/// <param name="interaction">Контекст взаимодействия с текстом сообщения.</param>
	/// <returns>Задача показа диалога.</returns>
	public async Task ShowQuestionMessageAsync(IInteractionContext<MessageContent, bool> interaction)
	{
		var box = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(
			new MsBox.Avalonia.Dto.MessageBoxStandardParams
			{
				ButtonDefinitions = ButtonEnum.YesNo,
				Icon = Icon.Question,
				ContentTitle = interaction.Input.Title,
				ContentMessage = interaction.Input.Message,
				WindowStartupLocation = WindowStartupLocation.CenterScreen,
				WindowIcon = IconProvider.Icon,
			});

		var result = _topLevel is Window window
			? await box.ShowWindowDialogAsync(window)
			: await box.ShowAsPopupAsync(_topLevel);

		interaction.SetOutput(result == ButtonResult.Yes);
	}

	/// <summary>Закрывает наложение или окно-владелец.</summary>
	/// <param name="interaction">Контекст взаимодействия закрытия.</param>
	public void CloseWindow(IInteractionContext<bool, bool> interaction)
	{
		if(_topLevel is Window window)
			window.Close();
		else
			_clearOverlay?.Invoke();

		interaction.SetOutput(true);
	}
}
