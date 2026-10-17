using System;
using System.Reactive;
using System.Threading.Tasks;

using Avalonia.Controls;

using MsBox.Avalonia.Enums;

using ReactiveUI;

using PostSys.Gui.Common.Helpers;
using PostSys.Gui.Common.Services.IconProvider;
using PostSys.Gui.Common.ViewModels;

namespace PostSys.Gui.Common.Services.Dialogs;

/// <summary>Обработчик показа сообщений и диалогов для desktop/mobile.</summary>
public class DialogReactiveWindow
{
	private readonly TopLevel _topLevel;
	private readonly Action<object>? _showOverlay;
	private readonly Action? _clearOverlay;

	/// <summary>Создаёт обработчик диалогов.</summary>
	public DialogReactiveWindow(
		TopLevel topLevel,
		Action<object>? showOverlay = null,
		Action? clearOverlay = null)
	{
		_topLevel = topLevel ?? throw new ArgumentNullException(nameof(topLevel));
		_showOverlay = showOverlay;
		_clearOverlay = clearOverlay;
	}

	/// <summary>Провайдер иконки приложения.</summary>
	public IApplicationIconProvider IconProvider { get; set; } = null!;

	/// <summary>Показывает диалоговое окно или overlay на mobile.</summary>
	public async Task ShowDialogOrOverlayAsync<TViewModel, TDialogWindow>(
		IInteractionContext<TViewModel, Unit?> interaction)
		where TViewModel : ViewModelBase
		where TDialogWindow : Window, new()
	{
		if(_topLevel is Window owner)
		{
			var dialog = new TDialogWindow { DataContext = interaction.Input };
			var result = await dialog.ShowDialog<Unit?>(owner);
			interaction.SetOutput(result);
			return;
		}

		_showOverlay?.Invoke(interaction.Input!);
		interaction.SetOutput(Unit.Default);
	}

	/// <summary>Показывает сообщение с описанием ошибки.</summary>
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

	/// <summary>Закрытие окна-владельца или overlay.</summary>
	public void CloseWindow(IInteractionContext<bool, bool> interaction)
	{
		if(_topLevel is Window window)
			window.Close();
		else
			_clearOverlay?.Invoke();

		interaction.SetOutput(true);
	}
}
