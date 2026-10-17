using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using Avalonia.Threading;

using NLog;

using ReactiveUI;

using PostSys.Client.Data.SignalR;
using PostSys.Client.Data.Subscriptions;
using PostSys.Client.GraphQl;
using PostSys.Gui.Common.Services.Dialogs;
using PostSys.Gui.Common.ViewModels;
using PostSys.Gui.Configuration;
using PostSys.Gui.Helpers;
using PostSys.Gui.Services.Storage;

namespace PostSys.Gui.ViewModels;

/// <summary>Модель представления главного экрана.</summary>
public class MainWindowViewModel : ViewModelBase, IActivatableViewModel, IDisposable
{
	private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

	private bool _firstSignalRState = true;
	private bool _isEnabledComponents;
	private bool _isServiceOnline;
	private string _connectionStatusText = Localization.UiLocalizer.Get("Connection_Connecting");
	private object _currentView = null!;
	private object? _overlayView;
	private AuthorizationViewModel _authorizationViewModel = null!;
	private OrdersViewModel _ordersViewModel = null!;
	private CurrentOrderViewModel _currentOrderViewModel = null!;
	private ButtonsViewModel _buttonsViewModel = null!;
	private readonly ISignalRClient _signalRClient;
	private readonly ApplicationConfiguration _appConfiguration;
	private readonly PostmanHelper _postmanHelper;
	private readonly GraphQlConnection _graphQlConnection;
	private readonly ISecureSessionStore _sessionStore;
	private readonly SettingsWindowViewModel _settingsWindowViewModel;
	private readonly InfoWindowViewModel _infoWindowViewModel;
	private readonly CancellationTokenSource _lifetimeCts = new();
	private bool _isCleanedUp;

	/// <summary>Настройки.</summary>
	public ICommand SettingsCommand { get; }

	/// <summary>О программе.</summary>
	public ICommand InfoCommand { get; }

	/// <summary>Повторить подключение к службе.</summary>
	public ICommand RetryConnectionCommand { get; }

	/// <inheritdoc/>
	public ViewModelActivator Activator { get; }

	/// <summary>Доступность элементов интерфейса при связи со службой или локальных данных заказов.</summary>
	public bool IsEnabledComponents
	{
		get => _isEnabledComponents;
		set => this.RaiseAndSetIfChanged(ref _isEnabledComponents, value);
	}

	/// <summary>Признак доступности службы на сервере.</summary>
	public bool IsServiceOnline
	{
		get => _isServiceOnline;
		set => this.RaiseAndSetIfChanged(ref _isServiceOnline, value);
	}

	/// <summary>Текст статуса соединения.</summary>
	public string ConnectionStatusText
	{
		get => _connectionStatusText;
		set => this.RaiseAndSetIfChanged(ref _connectionStatusText, value);
	}

	/// <summary>Текущий экран.</summary>
	public object CurrentView
	{
		get => _currentView;
		set
		{
			ArgumentNullException.ThrowIfNull(value);
			if(_currentView is null || _currentView.GetType() != value.GetType())
			{
				_currentView = value;
				this.RaisePropertyChanged();
			}
		}
	}

	/// <summary>Модель представления наложения поверх текущего экрана.</summary>
	public object? OverlayView
	{
		get => _overlayView;
		set => this.RaiseAndSetIfChanged(ref _overlayView, value);
	}

	/// <summary>Модель представления окна настроек.</summary>
	public SettingsWindowViewModel SettingsWindowViewModel => _settingsWindowViewModel;

	/// <summary>Модель представления экрана входа.</summary>
	public AuthorizationViewModel AuthorizationViewModel
	{
		get => _authorizationViewModel;
		set => this.RaiseAndSetIfChanged(ref _authorizationViewModel, value);
	}

	/// <summary>Модель представления списка заказов.</summary>
	public OrdersViewModel OrdersViewModel
	{
		get => _ordersViewModel;
		set => this.RaiseAndSetIfChanged(ref _ordersViewModel, value);
	}

	/// <summary>Модель представления текущего заказа.</summary>
	public CurrentOrderViewModel CurrentOrderViewModel
	{
		get => _currentOrderViewModel;
		set => this.RaiseAndSetIfChanged(ref _currentOrderViewModel, value);
	}

	/// <summary>Модель представления панели навигации.</summary>
	public ButtonsViewModel ButtonsViewModel
	{
		get => _buttonsViewModel;
		set => this.RaiseAndSetIfChanged(ref _buttonsViewModel, value);
	}

	/// <summary>Взаимодействие для показа окна настроек.</summary>
	public Interaction<SettingsWindowViewModel, Unit?> ShowSettingsWindow { get; } = new();

	/// <summary>Взаимодействие для показа окна «О программе».</summary>
	public Interaction<InfoWindowViewModel, Unit?> ShowInfoWindow { get; } = new();

	/// <summary>Набор взаимодействий служебных диалогов.</summary>
	public DialogServiceInteractions DialogServiceInteractions { get; init; } = new();

	/// <summary>Создаёт модель представления главного экрана.</summary>
	/// <param name="signalRClient">Клиент уведомлений о состоянии службы.</param>
	/// <param name="subscriptionClient">Клиент подписок на события.</param>
	/// <param name="postmenMessageHandler">Обработчик уведомлений о почтальонах.</param>
	/// <param name="appConfiguration">Конфигурация приложения.</param>
	/// <param name="postmanHelper">Данные текущего почтальона.</param>
	/// <param name="graphQlConnection">Общее подключение к службе.</param>
	/// <param name="sessionStore">Защищённое хранилище сеанса.</param>
	/// <param name="settingsWindowViewModel">Модель представления настроек.</param>
	/// <param name="infoWindowViewModel">Модель представления «О программе».</param>
	public MainWindowViewModel(
		ISignalRClient signalRClient,
		ISubscriptionClient subscriptionClient,
		IPostmenMessageHandler postmenMessageHandler,
		ApplicationConfiguration appConfiguration,
		PostmanHelper postmanHelper,
		GraphQlConnection graphQlConnection,
		ISecureSessionStore sessionStore,
		SettingsWindowViewModel settingsWindowViewModel,
		InfoWindowViewModel infoWindowViewModel)
	{
		Activator = new();
		_appConfiguration = appConfiguration;
		_postmanHelper = postmanHelper;
		_graphQlConnection = graphQlConnection;
		_sessionStore = sessionStore;
		_settingsWindowViewModel = settingsWindowViewModel;
		_infoWindowViewModel = infoWindowViewModel;
		_signalRClient = signalRClient;
		_signalRClient.ChangeServiceStatusEvent += OnChangeServiceStatusAsync;

		_ = _signalRClient.StartAsync(appConfiguration.ServiceAddress, _lifetimeCts.Token);

		SettingsCommand = ReactiveCommand.CreateFromTask(async () =>
			await ShowSettingsWindow.Handle(settingsWindowViewModel));
		InfoCommand = ReactiveCommand.CreateFromTask(async () =>
			await ShowInfoWindow.Handle(infoWindowViewModel));
		RetryConnectionCommand = ReactiveCommand.CreateFromTask(RetryConnectionAsync);

		this.WhenActivated(d =>
		{
			_ = ActivateAsync(d, subscriptionClient, postmenMessageHandler, settingsWindowViewModel);
		});
	}

	/// <summary>Показывает наложение с указанной моделью представления.</summary>
	/// <param name="viewModel">Модель представления для отображения поверх экрана.</param>
	public void ShowOverlay(object viewModel) => OverlayView = viewModel;

	/// <summary>Скрывает наложение.</summary>
	public void ClearOverlay() => OverlayView = null;

	/// <summary>Применяет адрес службы из внешней ссылки и открывает настройки.</summary>
	/// <param name="serviceAddress">Абсолютный адрес службы.</param>
	public void ApplyDeepLinkServiceAddress(string serviceAddress)
	{
		if(!Uri.TryCreate(serviceAddress, UriKind.Absolute, out _))
			return;

		_settingsWindowViewModel.ServiceAddress = serviceAddress.TrimEnd('/');
		ShowOverlay(_settingsWindowViewModel);
	}

	private async Task ActivateAsync(
		Action<IDisposable> d,
		ISubscriptionClient subscriptionClient,
		IPostmenMessageHandler postmenMessageHandler,
		SettingsWindowViewModel settingsWindowViewModel)
	{
		CurrentView = AuthorizationViewModel;
		AuthorizationViewModel.SuccessfulAuthorizationEvent += OnSuccessfulAuthorization;
		OrdersViewModel.ClearCurrentOrderEvent += OnClearingCurrentOrder;
		ButtonsViewModel.ButtonClickedEvent += OnClickedButton;
		settingsWindowViewModel.ChangeConfigurationEvent += OnChangeConfigurationAsync;
		settingsWindowViewModel.UiLanguageChanged += OnUiLanguageChanged;
		postmenMessageHandler.DeletedPostmanEvent += OnDeletedPostman;

		await subscriptionClient.StartConsuming();

		d(Disposable.Create(() =>
		{
			AuthorizationViewModel.SuccessfulAuthorizationEvent -= OnSuccessfulAuthorization;
			OrdersViewModel.ClearCurrentOrderEvent -= OnClearingCurrentOrder;
			ButtonsViewModel.ButtonClickedEvent -= OnClickedButton;
			settingsWindowViewModel.ChangeConfigurationEvent -= OnChangeConfigurationAsync;
			settingsWindowViewModel.UiLanguageChanged -= OnUiLanguageChanged;
			postmenMessageHandler.DeletedPostmanEvent -= OnDeletedPostman;
			subscriptionClient.StopConsuming();
			CleanupLifetimeBindings();
		}));
	}

	private void OnUiLanguageChanged(object? sender, EventArgs e)
	{
		ButtonsViewModel.RefreshTitles();
		_infoWindowViewModel.RefreshLocalizedTexts();
		ConnectionStatusText = IsServiceOnline
			? Localization.UiLocalizer.Get("Connection_Online")
			: Localization.UiLocalizer.Get("Connection_Offline");
	}

	/// <summary>Освобождает подписки на уведомления службы и токен времени жизни.</summary>
	public void Dispose()
	{
		CleanupLifetimeBindings();
		GC.SuppressFinalize(this);
	}

	private void CleanupLifetimeBindings()
	{
		if(_isCleanedUp)
			return;

		_isCleanedUp = true;
		_signalRClient.ChangeServiceStatusEvent -= OnChangeServiceStatusAsync;

		try
		{
			_lifetimeCts.Cancel();
			_lifetimeCts.Dispose();
		}
		catch(ObjectDisposedException)
		{
		}
	}

	private async Task RetryConnectionAsync()
	{
		ConnectionStatusText = Localization.UiLocalizer.Get("Connection_Reconnecting");
		try
		{
			await _signalRClient.RestartAsync(_appConfiguration.ServiceAddress, _lifetimeCts.Token);
		}
		catch(Exception ex)
		{
			Log.Error(ex, "Manual reconnect failed.");
			ConnectionStatusText = Localization.UiLocalizer.Get("Connection_Offline");
		}
	}

	private void OnSuccessfulAuthorization(object? sender, bool result)
	{
		CurrentView = OrdersViewModel;
		ButtonsViewModel.SetButtonEnabled(new()
		{
			{ "Profile", false },
			{ "Orders", true },
			{ "CurrentOrder", true }
		});
	}

	private void OnClickedButton(object? sender, string buttonKey)
	{
		CurrentView = buttonKey switch
		{
			"Profile" => AuthorizationViewModel,
			"Orders" => OrdersViewModel,
			_ => CurrentOrderViewModel
		};
	}

	private void OnClearingCurrentOrder(object? sender, Guid orderId) => CurrentOrderViewModel.Clear();

	private async void OnChangeConfigurationAsync(object? sender, bool isChanged)
	{
		ClearOverlay();
		_graphQlConnection.SetAccessToken(null);
		_postmanHelper.Clear();
		await _sessionStore.ClearAsync();

		await DialogServiceInteractions.ShowErrorMessage.Handle(new()
		{
			Title = Localization.UiLocalizer.Get("Title_Warning"),
			Message = Localization.UiLocalizer.Get("Msg_ServiceAddressChanged")
		});

		CurrentView = AuthorizationViewModel;
		ButtonsViewModel.SetButtonEnabled(new()
		{
			{ "Profile", false },
			{ "Orders", false },
			{ "CurrentOrder", false }
		});
	}

	private async void OnChangeServiceStatusAsync(object? sender, bool isAvailableService)
	{
		await Dispatcher.UIThread.InvokeAsync(() =>
		{
			IsServiceOnline = isAvailableService;
			IsEnabledComponents = isAvailableService || OrderDescriptionHasOfflineData();
			ConnectionStatusText = isAvailableService
				? Localization.UiLocalizer.Get("Connection_Online")
				: Localization.UiLocalizer.Get("Connection_Offline");

			if(_firstSignalRState && isAvailableService)
			{
				_firstSignalRState = false;
				return;
			}

			if(!isAvailableService)
				_firstSignalRState = false;
		});

		if(isAvailableService)
			Log.Info("Service is available.");
		else
			Log.Warn("Service is not available.");
	}

	private bool OrderDescriptionHasOfflineData() =>
		OrdersViewModel?.OrderDescriptionViewModels is { Count: > 0 };

	private async void OnDeletedPostman(object? sender, Guid deletedPostmanId)
	{
		if(deletedPostmanId != _postmanHelper.PostmanId)
			return;

		ClearOverlay();
		_graphQlConnection.SetAccessToken(null);
		_postmanHelper.Clear();
		await _sessionStore.ClearAsync();

		await DialogServiceInteractions.ShowErrorMessage.Handle(new()
		{
			Title = Localization.UiLocalizer.Get("Title_Error"),
			Message = Localization.UiLocalizer.Get("Msg_AccountDeleted")
		});

		CurrentView = AuthorizationViewModel;
		ButtonsViewModel.SetButtonEnabled(new()
		{
			{ "Profile", false },
			{ "Orders", false },
			{ "CurrentOrder", false }
		});
	}
}
