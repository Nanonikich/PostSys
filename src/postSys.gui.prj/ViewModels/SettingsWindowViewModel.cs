using System;
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

namespace PostSys.Gui.ViewModels;

/// <summary>Модель представления окна настроек.</summary>
public class SettingsWindowViewModel : ViewModelBase
{
	private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

	private string _serviceAddress;
	private string _yandexApiKey;
	private readonly ApplicationConfiguration _configuration;
	private readonly GraphQlConnection _graphQlConnection;
	private readonly ISignalRClient _signalRClient;
	private readonly ISubscriptionClient _subscriptionClient;
	private readonly IAddressClient _addressClient;

	/// <summary>Возвращает команду применения новых настроек.</summary>
	public ICommand ApplySettingsCommand { get; }

	/// <summary>Возвращает или задаёт адрес подключения к сервису.</summary>
	public string ServiceAddress
	{
		get => _serviceAddress;
		set => this.RaiseAndSetIfChanged(ref _serviceAddress, value);
	}

	/// <summary>Возвращает или задаёт ключ к Яндекс.API.</summary>
	public string YandexApiKey
	{
		get => _yandexApiKey;
		set => this.RaiseAndSetIfChanged(ref _yandexApiKey, value);
	}

	/// <summary>Возвращает взаимодействие с диалоговыми окнами.</summary>
	public DialogServiceInteractions DialogServiceInteractions { get; init; } = new();

	/// <summary>Событие изменения конфигурации.</summary>
	public event EventHandler<bool>? ChangeConfigurationEvent;

	/// <summary>Создаёт экземпляр класса <see cref="SettingsWindowViewModel"/>.</summary>
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

		ApplySettingsCommand = ReactiveCommand.CreateFromTask(ApplySettingsAsync);
	}

	private async Task ApplySettingsAsync()
	{
		var isCorrectKernelServiceAddress = Uri.TryCreate(ServiceAddress, UriKind.Absolute, out _);
		var isCorrectYandexApiKey = Guid.TryParse(_yandexApiKey, out var parsedApiKey);

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

		var addressChanged = !string.Equals(_configuration.ServiceAddress, ServiceAddress, StringComparison.OrdinalIgnoreCase);
		var apiKeyChanged = _configuration.YandexApiKey != parsedApiKey;

		if(!addressChanged && !apiKeyChanged)
		{
			await DialogServiceInteractions.CloseWindow.Handle(true);
			return;
		}

		_configuration.ServiceAddress = ServiceAddress.TrimEnd('/');
		_configuration.YandexApiKey = parsedApiKey;
		_configuration.Save(ProfileLocationStorage.ConfigPath);

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
