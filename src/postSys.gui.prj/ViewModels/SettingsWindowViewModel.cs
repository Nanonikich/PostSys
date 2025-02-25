using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using NLog;

using ReactiveUI;

using PostSys.Framework.Common;
using PostSys.Gui.Common.Services.Dialogs;
using PostSys.Gui.Common.ViewModels;
using PostSys.Gui.Configuration;

namespace PostSys.Gui.ViewModels;

/// <summary>Модель представления окна настроек.</summary>
public class SettingsWindowViewModel : ViewModelBase
{
	#region Static

	private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

	#endregion

	#region Data

	private string _serviceAddress;
	private string _yandexApiKey;

	#endregion

	#region Properties

	#region Commands

	/// <summary>Возвращает команду применения новых настроек.</summary>
	/// <value>Команда применения новых настроек.</value>
	public ICommand ApplySettingsCommand { get; }

	#endregion

	/// <summary>Возвращает или задаёт адрес подключения к сервису.</summary>
	/// <value>Адрес подключения к сервису.</value>
	public string ServiceAddress
	{
		get => _serviceAddress;
		set => this.RaiseAndSetIfChanged(ref _serviceAddress, value);
	}

	/// <summary>Возвращает или задаёт ключ к Яндекс.API.</summary>
	/// <value>Ключ к Яндекс.API.</value>
	public string YandexApiKey
	{
		get => _yandexApiKey;
		set => this.RaiseAndSetIfChanged(ref _yandexApiKey, value);
	}

	/// <summary>Возвращает взаимодействие с диалоговыми окнами.</summary>
	/// <value>Взаимодействие с диалоговыми окнами.</value>
	public DialogServiceInteractions DialogServiceInteractions { get; init; } = new();

	#endregion

	#region Events

	/// <summary>Событие изменения конфигурации.</summary>
	public event EventHandler<bool> ChangeConfigurationEvent;

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="SettingsWindowViewModel"/>.</summary>
	/// <param name="configuration">Конфигурация приложения.</param>
	public SettingsWindowViewModel(ApplicationConfiguration configuration)
	{
		ServiceAddress = configuration.ServiceAddress;
		YandexApiKey = configuration.YandexApiKey.ToString();

		ApplySettingsCommand = ReactiveCommand.CreateFromTask(async () => await ApplySettingsAsync(configuration));
	}

	#endregion

	#region Methods

	private async Task ApplySettingsAsync(ApplicationConfiguration configuration)
	{
		var isCorrectKernelServiceAddress = Uri.TryCreate(ServiceAddress, UriKind.Absolute, out _);
		var isCorrectYandexApiKey = Guid.TryParse(_yandexApiKey, out _);

		if(!isCorrectKernelServiceAddress)
		{
			await DialogServiceInteractions.ShowErrorMessage.Handle(new()
			{
				Title = "Validation Error",
				Message = "Service address is incorrect."
			});
			Log.Warn("An incorrect address service was entered.");

			return;
		}

		if(!isCorrectYandexApiKey)
		{
			await DialogServiceInteractions.ShowErrorMessage.Handle(new()
			{
				Title = "Validation Error",
				Message = "Yandex.Api key is incorrect."
			});
			Log.Warn("An incorrect Yandex.Api key was entered.");

			return;
		}

		if(configuration.ServiceAddress != ServiceAddress
			|| configuration.YandexApiKey.ToString() != YandexApiKey)
		{
			ChangeConfigurationEvent?.Invoke(this, true);
		}

		configuration.ServiceAddress = ServiceAddress;
		configuration.YandexApiKey = Guid.Parse(_yandexApiKey);
		configuration.Save(ProfileLocationStorage.ConfigPath);

		await DialogServiceInteractions.CloseWindow.Handle(true);
	}

	#endregion
}
