using System.Reactive;
using System.Threading.Tasks;

using Avalonia.Controls;

using MsBox.Avalonia.Enums;

using ReactiveUI;

using PostSys.Gui.Common.Helpers;
using PostSys.Gui.Common.Services.IconProvider;
using PostSys.Gui.Common.ViewModels;

namespace PostSys.Gui.Common.Services.Dialogs;

/// <summary>Обработчик показа сообщений.</summary>
/// <param name="window">Окно-родитель.</param>
public class DialogReactiveWindow(Window window)
{
	#region Properties

	/// <summary>Возвращает или задаёт провайдер иконки приложения.</summary>
	/// <value>Провайдер иконки приложения.</value>
	public IApplicationIconProvider IconProvider { get; set; }

	#endregion

	#region Methods

	/// <summary>Показывает диалоговое окно.</summary>
	/// <typeparam name="TViewModel">Модель представления вызываемого окна.</typeparam>
	/// <typeparam name="TDialogWindow">Окно модели представления.</typeparam>
	/// <param name="interaction">Связь для вызова.</param>
	/// <returns>Асинхронная операция.</returns>
	public async Task ShowDialogWindowAsync<TViewModel, TDialogWindow>(IInteractionContext<TViewModel, Unit?> interaction)
		where TViewModel : ViewModelBase
		where TDialogWindow : Window, new()
	{
		var dialog = new TDialogWindow
		{
			DataContext = interaction.Input
		};

		var result = await dialog.ShowDialog<Unit?>(window);
		interaction.SetOutput(result);
	}

	/// <summary>Показывает сообщение с описанием ошибки.</summary>
	/// <param name="interaction">Связь для вызова.</param>
	/// <returns>Асинхронная операция.</returns>
	public async Task ShowErrorMessageAsync(IInteractionContext<MessageContent, bool> interaction)
	{
		var msBoxStandardWindow = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(
			new MsBox.Avalonia.Dto.MessageBoxStandardParams
			{
				ButtonDefinitions = ButtonEnum.Ok,
				Icon = Icon.Error,
				ContentTitle = interaction.Input.Title,
				ContentMessage = interaction.Input.Message,
				WindowStartupLocation = WindowStartupLocation.CenterScreen,
				WindowIcon = IconProvider.Icon,
			});

		interaction.SetOutput(await msBoxStandardWindow.ShowWindowDialogAsync(window) == ButtonResult.Ok);
	}

	/// <summary>Показывает сообщение с вопросом.</summary>
	/// <param name="interaction">Связь для вызова.</param>
	/// <returns>Асинхронная операция.</returns>
	public async Task ShowQuestionMessageAsync(IInteractionContext<MessageContent, bool> interaction)
	{
		var msBoxStandardWindow = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(
			new MsBox.Avalonia.Dto.MessageBoxStandardParams
			{
				ButtonDefinitions = ButtonEnum.YesNo,
				Icon = Icon.Question,
				ContentTitle = interaction.Input.Title,
				ContentMessage = interaction.Input.Message,
				WindowStartupLocation = WindowStartupLocation.CenterScreen,
				WindowIcon = IconProvider.Icon,
			});

		interaction.SetOutput(await msBoxStandardWindow.ShowWindowDialogAsync(window) == ButtonResult.Yes);
	}

	/// <summary>Закрытие окна.</summary>
	/// <param name="interaction">Установка вывода.</param>
	public void CloseWindow(IInteractionContext<bool, bool> interaction)
	{
		window?.Close();
		interaction.SetOutput(true);
	}

	#endregion
}