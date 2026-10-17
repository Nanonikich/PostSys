using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using NLog;

using ReactiveUI;

using PostSys.Client.Data;
using PostSys.Client.Data.SignalR;
using PostSys.Client.Data.Subscriptions;
using PostSys.Client.GraphQl;
using PostSys.Framework.Common;
using PostSys.Gui.Common.Services.Dialogs;
using PostSys.Gui.Common.ViewModels;
using PostSys.Gui.Configuration;
using PostSys.Gui.Localization;

namespace PostSys.Gui.ViewModels;

/// <summary>Модель представления экрана настроек.</summary>
public class SettingsWindowViewModel : ViewModelBase
{
	private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

	private string _serviceAddress = string.Empty;
	private string _yandexApiKey = string.Empty;
	private LanguageOption _selectedLanguage = null!;
	private readonly ApplicationConfiguration _configuration;
	private readonly GraphQlConnection _graphQlConnection;
	private readonly ISignalRClient _signalRClient;
	private readonly ISubscriptionClient _subscriptionClient;
	private readonly IAddressClient _addressClient;

	/// <summary>Команда применения настроек.</summary>
	public ICommand ApplySettingsCommand { get; }

	/// <summary>Адрес службы.</summary>
	public string ServiceAddress
	{
		get => _serviceAddress;
		set => this.RaiseAndSetIfChanged(ref _serviceAddress, value);
	}

	/// <summary>Ключ Яндекс.API.</summary>
	public string YandexApiKey
	{
		get => _yandexApiKey;
		set => this.RaiseAndSetIfChanged(ref _yandexApiKey, value);
	}

	/// <summary>Выбранный язык интерфейса.</summary>
	public LanguageOption SelectedLanguage
	{
		get => _selectedLanguage;
		set => this.RaiseAndSetIfChanged(ref _selectedLanguage, value);
	}

	/// <summary>Доступные языки интерфейса.</summary>
	public IReadOnlyList<LanguageOption> LanguageOptions { get; private set; } = [];

	/// <summary>Взаимодействия с диалоговыми окнами.</summary>
	public DialogServiceInteractions DialogServiceInteractions { get; init; } = new();

	/// <summary>Событие изменения настроек подключения к службе.</summary>
	public event EventHandler<bool>? ChangeConfigurationEvent;

	/// <summary>Событие смены языка интерфейса.</summary>
	public event EventHandler? UiLanguageChanged;

	/// <summary>Создаёт экземпляр класса <see cref="SettingsWindowViewModel"/>.</summary>
	/// <param name="configuration">Конфигурация приложения.</param>
	/// <param name="graphQlConnection">Общее подключение к службе.</param>
	/// <param name="signalRClient">Клиент уведомлений о состоянии службы.</param>
	/// <param name="subscriptionClient">Клиент подписок на события.</param>
	/// <param name="addressClient">Клиент геокодирования.</param>
	public SettingsWindowViewModel(
		ApplicationConfiguration configuration,
		GraphQlConnection graphQlConnection,
		ISignalRClient signalRClient,
		ISubscriptionClient subscriptionClient,
		IAddressClient addressClient)
	{
		_configuration = configuration;
		_graphQlConnection = graphQlConnection;
		_signalRClient = signalRClient;
		_subscriptionClient = subscriptionClient;
		_addressClient = addressClient;

		ServiceAddress = configuration.ServiceAddress;
		YandexApiKey = configuration.YandexApiKey.ToString();
		RefreshLanguageOptions(configuration.UiLanguage);

		ApplySettingsCommand = ReactiveCommand.CreateFromTask(ApplySettingsAsync);
	}

	private void RefreshLanguageOptions(string? preferredCode)
	{
		LanguageOptions =
		[
			new("ru-RU", UiLocalizer.Get("Language_Ru")),
			new("en", UiLocalizer.Get("Language_En"))
		];
		this.RaisePropertyChanged(nameof(LanguageOptions));

		var code = string.IsNullOrWhiteSpace(preferredCode) ? UiLocalizer.DefaultCultureName : preferredCode;
		SelectedLanguage = LanguageOptions.FirstOrDefault(x =>
			string.Equals(x.Code, code, StringComparison.OrdinalIgnoreCase))
			?? LanguageOptions[0];
	}

	private async Task ApplySettingsAsync()
	{
		var isCorrectKernelServiceAddress = Uri.TryCreate(ServiceAddress, UriKind.Absolute, out _);
		var isCorrectYandexApiKey = Guid.TryParse(_yandexApiKey, out var parsedApiKey);

		if(!isCorrectKernelServiceAddress)
		{
			await DialogServiceInteractions.ShowErrorMessage.Handle(new()
			{
				Title = UiLocalizer.Get("Title_ValidationError"),
				Message = UiLocalizer.Get("Msg_ServiceAddressInvalid")
			});
			Log.Warn("An incorrect address service was entered.");
			return;
		}

		if(!isCorrectYandexApiKey)
		{
			await DialogServiceInteractions.ShowErrorMessage.Handle(new()
			{
				Title = UiLocalizer.Get("Title_ValidationError"),
				Message = UiLocalizer.Get("Msg_YandexKeyInvalid")
			});
			Log.Warn("An incorrect Yandex.Api key was entered.");
			return;
		}

		var addressChanged = !string.Equals(_configuration.ServiceAddress, ServiceAddress, StringComparison.OrdinalIgnoreCase);
		var apiKeyChanged = _configuration.YandexApiKey != parsedApiKey;
		var languageCode = SelectedLanguage.Code;
		var languageChanged = !string.Equals(_configuration.UiLanguage, languageCode, StringComparison.OrdinalIgnoreCase);

		if(!addressChanged && !apiKeyChanged && !languageChanged)
		{
			await DialogServiceInteractions.CloseWindow.Handle(true);
			return;
		}

		_configuration.ServiceAddress = ServiceAddress.TrimEnd('/');
		_configuration.YandexApiKey = parsedApiKey;
		_configuration.UiLanguage = languageCode;
		_configuration.Save(ProfileLocationStorage.ConfigPath);

		if(languageChanged)
		{
			UiLocalizer.ApplyCulture(languageCode);
			RefreshLanguageOptions(languageCode);
			UiLanguageChanged?.Invoke(this, EventArgs.Empty);
		}

		if(apiKeyChanged)
			_addressClient.UpdateApiKey(parsedApiKey);

		if(addressChanged)
		{
			_graphQlConnection.UpdateBaseAddress(_configuration.ServiceAddress);

			try
			{
				await _signalRClient.RestartAsync(_configuration.ServiceAddress);
				await _subscriptionClient.RestartAsync();
			}
			catch(Exception ex)
			{
				Log.Error(ex, "Failed to reconnect after service address change.");
			}

			ChangeConfigurationEvent?.Invoke(this, true);
		}

		await DialogServiceInteractions.CloseWindow.Handle(true);
	}
}

/// <summary>Вариант языка интерфейса.</summary>
/// <param name="Code">Код культуры.</param>
/// <param name="DisplayName">Отображаемое имя.</param>
public sealed record LanguageOption(string Code, string DisplayName);
