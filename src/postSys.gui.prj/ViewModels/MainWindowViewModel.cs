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
using PostSys.Gui.Common.Services.Dialogs;
using PostSys.Gui.Common.ViewModels;
using PostSys.Gui.Helpers;

namespace PostSys.Gui.ViewModels;

/// <summary>Модель представления главного окна приложения.</summary>
public class MainWindowViewModel : ViewModelBase, IActivatableViewModel
{
	#region Statics

	private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

	#endregion

	#region Data

	private bool _firstSignalRState = true;
	private bool _isEnabledComponents;
	private object _currentView;
	private AuthorizationViewModel _authorizationViewModel;
	private OrdersViewModel _ordersViewModel;
	private CurrentOrderViewModel _currentOrderViewModel;
	private ButtonsViewModel _buttonsViewModel;
	private readonly ISignalRClient _signalRClient;
	private readonly PostmanHelper _postmanHelper;
	private readonly CancellationTokenSource _cancellationTokenSource = new();

	#endregion

	#region Properties

	#region Commands

	/// <summary>Возвращает команду открытия окна настроек приложения.</summary>
	/// <value>Команда открытия окна настроек приложения.</value>
	public ICommand SettingsCommand { get; }

	/// <summary>Возвращает команду кнопки, которая открывает окно "О программе".</summary>
	/// <value>Команда кнопки, которая открывает окно "О программе".</value>
	public ICommand InfoCommand { get; }

	#endregion

	/// <inheritdoc/>
	public ViewModelActivator Activator { get; }

	/// <summary>Возвращает или задаёт состояние компонентов окна.</summary>
	/// <value>Состояние компонентов окна.</value>
	public bool IsEnabledComponents
	{
		get => _isEnabledComponents;
		set => this.RaiseAndSetIfChanged(ref _isEnabledComponents, value);
	}

	/// <summary>Возвращает или задаёт выбранное представление.</summary>
	/// <value>Выбранное представление.</value>
	public object CurrentView
	{
		get => _currentView;
		set
		{
			if(_currentView == null || _currentView.GetType() != value.GetType())
				this.RaiseAndSetIfChanged(ref _currentView, value);
		}
	}

	/// <summary>Возвращает или задаёт модель представления авторизации.</summary>
	/// <value>Модель представления авторизации.</value>
	public AuthorizationViewModel AuthorizationViewModel
	{
		get => _authorizationViewModel;
		set => this.RaiseAndSetIfChanged(ref _authorizationViewModel, value);
	}

	/// <summary>Возвращает или задаёт модель представления списка заказов.</summary>
	/// <value>Модель представления списка заказов.</value>
	public OrdersViewModel OrdersViewModel
	{
		get => _ordersViewModel;
		set => this.RaiseAndSetIfChanged(ref _ordersViewModel, value);
	}

	/// <summary>Возвращает или задаёт модель представления текущего заказа.</summary>
	/// <value>Модель представления текущего заказа.</value>
	public CurrentOrderViewModel CurrentOrderViewModel
	{
		get => _currentOrderViewModel;
		set => this.RaiseAndSetIfChanged(ref _currentOrderViewModel, value);
	}

	/// <summary>Возвращает или задаёт модель представления основных кнопок.</summary>
	/// <value>Модель представления основных кнопок.</value>
	public ButtonsViewModel ButtonsViewModel
	{
		get => _buttonsViewModel;
		set => this.RaiseAndSetIfChanged(ref _buttonsViewModel, value);
	}

	/// <summary>Возвращает взаимодействие с окном настроек.</summary>
	/// <value>Взаимодействие с окном настроек.</value>
	public Interaction<SettingsWindowViewModel, Unit?> ShowSettingsWindow { get; } = new();

	/// <summary>Возвращает взаимодействие с окном "О программе".</summary>
	/// <value>Взаимодействие с окном "О программе".</value>
	public Interaction<InfoWindowViewModel, Unit?> ShowInfoWindow { get; } = new();

	/// <summary>Возвращает взаимодействие с диалоговыми окнами.</summary>
	/// <value>Взаимодействие с диалоговыми окнами.</value>
	public DialogServiceInteractions DialogServiceInteractions { get; init; } = new();

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="MainWindowViewModel"/>.</summary>
	/// <param name="signalRClient"><see cref="ISignalRClient"/>.</param>
	/// <param name="subscriptionClient"><see cref="ISubscriptionClient"/>.</param>
	/// <param name="postmenMessageHandler"><see cref="IPostmenMessageHandler"/>.</param>
	/// <param name="serviceAddress">Адрес сервиса.</param>
	/// <param name="postmanHelper">Данные об авторизованном почтальоне.</param>
	/// <param name="settingsWindowViewModel">Модель представления окна настроек.</param>
	/// <param name="infoWindowViewModel">Модель представления окна "О программе".</param>
	public MainWindowViewModel(
		ISignalRClient signalRClient,
		ISubscriptionClient subscriptionClient,
		IPostmenMessageHandler postmenMessageHandler,
		string serviceAddress,
		PostmanHelper postmanHelper,
		SettingsWindowViewModel settingsWindowViewModel,
		InfoWindowViewModel infoWindowViewModel)
	{
		Activator = new();

		_postmanHelper = postmanHelper;
		_signalRClient = signalRClient;
		_signalRClient.ChangeServiceStatusEvent += OnChangeServiceStatusAsync;

		Task.Run(async () => await _signalRClient.StartAsync(serviceAddress, _cancellationTokenSource.Token));
		
		SettingsCommand = ReactiveCommand.CreateFromTask(async () =>
			await ShowGeneralSettingsWindowAsync(settingsWindowViewModel));
		InfoCommand = ReactiveCommand.CreateFromTask(async () => 
			await ShowWindowAboutProgramAsync(infoWindowViewModel));

		this.WhenActivated(disposables =>
		{
			CurrentView = AuthorizationViewModel;
			AuthorizationViewModel.SuccessfulAuthorizationEvent += OnSuccessfulAuthorization;
			OrdersViewModel.ClearCurrentOrderEvent += OnClearingCurrentOrder;
			ButtonsViewModel.ButtonClickedEvent += OnClickedButton;
			settingsWindowViewModel.ChangeConfigurationEvent += OnChangeConfigurationAsync;
			postmenMessageHandler.DeletedPostmanEvent += OnDeletedPostman;

			subscriptionClient.StartConsuming();

			Disposable
				.Create(async () =>
				{
					AuthorizationViewModel.SuccessfulAuthorizationEvent -= OnSuccessfulAuthorization;
					OrdersViewModel.ClearCurrentOrderEvent -= OnClearingCurrentOrder;
					ButtonsViewModel.ButtonClickedEvent -= OnClickedButton;
					settingsWindowViewModel.ChangeConfigurationEvent -= OnChangeConfigurationAsync;
					postmenMessageHandler.DeletedPostmanEvent -= OnDeletedPostman;

					subscriptionClient.StopConsuming();

					_signalRClient.ChangeServiceStatusEvent -= OnChangeServiceStatusAsync;
					await _cancellationTokenSource.CancelAsync();
					_cancellationTokenSource?.Dispose();
					await _signalRClient.DisposeAsync();
				})
				.DisposeWith(disposables);
		});
	}

	#endregion

	#region Methods

	private async Task ShowGeneralSettingsWindowAsync(SettingsWindowViewModel settingsWindowViewModel) 
		=> await ShowSettingsWindow.Handle(settingsWindowViewModel);

	private async Task ShowWindowAboutProgramAsync(InfoWindowViewModel infoWindowViewModel) 
		=> await ShowInfoWindow.Handle(infoWindowViewModel);

	private async Task HandleServiceStatusChangeAsync(bool isAvailableService)
	{
		await Dispatcher.UIThread.InvokeAsync(async () =>
		{
			IsEnabledComponents = isAvailableService;

			await DialogServiceInteractions.ShowErrorMessage.Handle(new()
			{
				Title = isAvailableService ? "Attention" : "Error",
				Message = isAvailableService
					? "The service is available again. Restarting the application is recommended."
					: "Service is not available."
			});
		});
	}

	#endregion

	#region Handlers

	private void OnSuccessfulAuthorization(object? sender, bool result)
	{
		CurrentView = OrdersViewModel;
		ButtonsViewModel.SetButtonEnabled(new() 
		{ 
			{ "Profile", false },
			{ "Orders", true },
			{ "Current order", true }
		});
	}

	private void OnClickedButton(object? sender, string buttonTitle)
	{
		if(buttonTitle == "Profile")
		{
			CurrentView = AuthorizationViewModel;
		}
		else if(buttonTitle == "Orders")
		{
			CurrentView = OrdersViewModel;
		}
		else
		{
			CurrentView = CurrentOrderViewModel;
		}
	}

	private void OnClearingCurrentOrder(object? sender, Guid orderId) => CurrentOrderViewModel.Clear();

	private async void OnChangeConfigurationAsync(object? sender, bool isChanged)
	{
		await DialogServiceInteractions.ShowQuestionMessage.Handle(new()
		{
			Title = "Warning",
			Message = "Restart the application!"
		});

		await Task.Delay(3000);

		Environment.Exit(0);
	}

	private async void OnChangeServiceStatusAsync(object? sender, bool isAvailableService)
	{
		if(!IsEnabledComponents && isAvailableService)
		{
			if(_firstSignalRState)
			{
				await Dispatcher.UIThread.InvokeAsync(() => IsEnabledComponents = true);
				return;
			}

			await HandleServiceStatusChangeAsync(isAvailableService);
			Log.Info("Service status is available.");
		}
		else if(IsEnabledComponents && !isAvailableService)
		{
			_firstSignalRState = false;

			await HandleServiceStatusChangeAsync(isAvailableService);
			Log.Error("Service is not available.");
		}
	}

	private async void OnDeletedPostman(object? sender, Guid deletedPostmanId)
	{
		if(deletedPostmanId == _postmanHelper.PostmanId)
		{
			await DialogServiceInteractions.ShowQuestionMessage.Handle(new()
			{
				Title = "Error",
				Message = "Account has been deleted"
			});

			await Task.Delay(3000);

			Environment.Exit(0);
		}
	}

	#endregion
}