using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Avalonia.Collections;

using Microsoft.Extensions.DependencyInjection;

using NLog;

using ReactiveUI;

using PostSys.Client;
using PostSys.Client.Data;
using PostSys.Client.Data.Subscriptions;
using PostSys.Gui.Common.ViewModels;
using PostSys.Gui.Common.Services.Dialogs;
using PostSys.Gui.Configuration;
using PostSys.Gui.Helpers;
using PostSys.ReadModels;
using PostSys.ReadModels.Contracts;
using PostSys.ReadModels.Helpers;

namespace PostSys.Gui.ViewModels;

/// <summary>Модель представления заказов.</summary>
public class OrdersViewModel : ViewModelBase, IActivatableViewModel
{
	#region Static

	private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

	#endregion

	#region Data

	private AvaloniaList<OrderDescriptionViewModel> _orderDescriptionViewModels = [];
	private readonly IAddressClient _addressClient;
	private readonly IClientGraphQlClient _clientGraphQlClient;
	private readonly IPackageGraphQlClient _packageGraphQlClient;
	private readonly PostmanHelper _postmanHelper;
	private readonly ApplicationConfiguration _appConfiguration;
	private readonly IServiceProvider _serviceProvider;

	#endregion

	#region Properties

	/// <inheritdoc/>
	public ViewModelActivator Activator { get; }

	/// <summary>Возвращает взаимодействие с диалоговыми окнами.</summary>
	/// <value>Взаимодействие с диалоговыми окнами.</value>
	public DialogServiceInteractions DialogServiceInteractions { get; init; } = new();

	/// <summary>Возвращает или задаёт модели представления описаний заказов.</summary>
	/// <value>Модели представления описаний заказов.</value>
	public AvaloniaList<OrderDescriptionViewModel> OrderDescriptionViewModels
	{
		get => _orderDescriptionViewModels;
		set => this.RaiseAndSetIfChanged(ref _orderDescriptionViewModels, value);
	}

	#endregion

	#region Events

	/// <summary>Событие снятия выбранного заказа с почтальона.</summary>
	public event EventHandler<Guid> ClearCurrentOrderEvent;

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="OrdersViewModel"/>.</summary>
	/// <param name="postmanHelper">Данные об авторизованном почтальоне.</param>
	/// <param name="addressClient">Клиент получения адреса Яндекс.API.</param>
	/// <param name="clientGraphQlClient">Клиент GraphQL для сущности "Клиент".</param>
	/// <param name="packageGraphQlClient">Клиент GraphQL для сущности "Посылка".</param>
	/// <param name="clientsMessageHandler"><see cref="IClientsMessageHandler"/>.</param>
	/// <param name="packagesMessageHandler"><see cref="IPackagesMessageHandler"/>.</param>
	/// <param name="appConfiguration">Конфигурация приложения.</param>
	/// <param name="serviceProvider">Провайдер зарегистрированных сервисов.</param>
	public OrdersViewModel(
		PostmanHelper postmanHelper,
		IAddressClient addressClient,
		IClientGraphQlClient clientGraphQlClient,
		IPackageGraphQlClient packageGraphQlClient,
		IClientsMessageHandler clientsMessageHandler,
		IPackagesMessageHandler packagesMessageHandler,
		ApplicationConfiguration appConfiguration,
		IServiceProvider serviceProvider)
	{
		Activator = new();

		_postmanHelper = postmanHelper;
		_addressClient = addressClient;
		_clientGraphQlClient = clientGraphQlClient;
		_packageGraphQlClient = packageGraphQlClient;
		_appConfiguration = appConfiguration;
		_serviceProvider = serviceProvider;

		this.WhenActivated(async disposables =>
		{
			await InitOrderDescriptionViewModelsAsync();

			clientsMessageHandler.ChangedClientFullnameEvent += OnChangedClientFullname;
			clientsMessageHandler.ChangedClientPhoneNumberEvent += OnChangedClientPhoneNumber;

			packagesMessageHandler.CreatedPackageEvent += OnCreatedPackageAsync;
			packagesMessageHandler.ChangedPackageEvent += OnChangedPackageAsync;
			packagesMessageHandler.ChangedPackageAddressEvent += OnChangedPackageAddress;
			packagesMessageHandler.ChangedPackageDimensionsEvent += OnChangedPackageDimensions;
			packagesMessageHandler.ChangedPackageStatusEvent += OnChangedPackageStatus;
			packagesMessageHandler.DeletedPackageEvent += OnDeletedPackage;

			Disposable
				.Create(() =>
				{
					clientsMessageHandler.ChangedClientFullnameEvent -= OnChangedClientFullname;
					clientsMessageHandler.ChangedClientPhoneNumberEvent -= OnChangedClientPhoneNumber;

					packagesMessageHandler.CreatedPackageEvent -= OnCreatedPackageAsync;
					packagesMessageHandler.ChangedPackageEvent -= OnChangedPackageAsync;
					packagesMessageHandler.ChangedPackageAddressEvent -= OnChangedPackageAddress;
					packagesMessageHandler.ChangedPackageDimensionsEvent -= OnChangedPackageDimensions;
					packagesMessageHandler.ChangedPackageStatusEvent -= OnChangedPackageStatus;
					packagesMessageHandler.DeletedPackageEvent -= OnDeletedPackage;
				})
				.DisposeWith(disposables);
		});
	}

	#endregion

	#region Methods

	/// <summary>Загрузка списка заказов.</summary>
	private async Task InitOrderDescriptionViewModelsAsync()
	{
		try
		{
			var serviceAddress = _appConfiguration.ServiceAddress;

			var packages = await _packageGraphQlClient.GetPackagesByPostmanIdAsync(_postmanHelper.PostmanId);

			var clientIds = packages
				.Select(x => x.PackageClientId)
				.Distinct()
				.ToArray();

			var clients = await _clientGraphQlClient.GetClientsByIdsAsync(clientIds);

			var clientLookup = clients.ToLookup(x => x.Id);

			var addressTasks = packages.Select(async info =>
			{
				var clientInfo = clientLookup[info.PackageClientId].FirstOrDefault();
				if(clientInfo == null) return null;

				return (await LoadOrderDescriptionAsync(info, clientInfo));
			});

			var orderModels = (await Task.WhenAll(addressTasks)).Where(m => m != null);
			OrderDescriptionViewModels = new AvaloniaList<OrderDescriptionViewModel>(orderModels);
		}
		catch(GraphQlClientException graphQlEx)
		{
			await DialogServiceInteractions.ShowErrorMessage.Handle(new()
			{
				Title = "GraphQL Error",
				Message = graphQlEx.Message
			});
			Log.Error(graphQlEx, "GraphQL error occurred while initializing orders.");
		}
		catch(Exception ex)
		{
			await DialogServiceInteractions.ShowErrorMessage.Handle(new()
			{
				Title = "Unexpected Error",
				Message = "An unexpected error occurred while loading orders. Please try again later."
			});
			Log.Error(ex, "Unexpected error occurred while initializing orders.");
		}
	}

	/// <summary>Задаёт описание заказа в модель представления.</summary>
	/// <param name="newData">Посылка.</param>
	/// <param name="clientInfo">Данные о клиенте.</param>
	/// <returns>Асинхронная операция, возвращающая модель представления с описанием заказа.</returns>
	private async Task<OrderDescriptionViewModel?> LoadOrderDescriptionAsync(Package newData, ReadModels.Client clientInfo)
	{
		var orderDescriptionViewModel = _serviceProvider.GetRequiredService<OrderDescriptionViewModel>();
		orderDescriptionViewModel.ShowErrorMessage = DialogServiceInteractions.ShowErrorMessage;
		orderDescriptionViewModel.PackageId = newData.Id;
		orderDescriptionViewModel.ImagePath = GetOrderImage(newData.Dimensions);
		orderDescriptionViewModel.Status = newData.Status;
		orderDescriptionViewModel.ClientInfo = new()
		{
			Id = newData.PackageClientId,
			Fullname = clientInfo.Fullname,
			PhoneNumber = clientInfo.PhoneNumber
		};

		try
		{
			orderDescriptionViewModel.Address = await _addressClient.GetAddressAsync(
				newData.Address.Latitude,
				newData.Address.Longitude);

			return orderDescriptionViewModel;
		}
		catch(Exception addrEx)
		{
			await DialogServiceInteractions.ShowErrorMessage.Handle(new()
			{
				Title = "Address Retrieval Error",
				Message = "Failed to retrieve the address for the order. Please verify the coordinates."
			});
			Log.Error(addrEx, "Error retrieving address for package ID {PackageId}.", newData.Id);
			orderDescriptionViewModel.Address = "Address not available";
		}

		return null;
	}

	/// <summary>Получает путь к изображению заказа.</summary>
	/// <param name="dimensions">Параметры посылки.</param>
	/// <returns>Путь к изображению заказа.</returns>
	private string GetOrderImage(ReadModels.Helpers.Dimensions dimensions)
	{
		if(dimensions.Weight > 1.0 || dimensions.Length > 20.0 || dimensions.Height > 20.0)
		{
			return "/Assets/Packages/box.png";
		}

		return "/Assets/Packages/envelope.png";
	}

	#endregion

	#region Handlers

	private void OnChangedClientFullname(object? sender, KeyValuePair<Guid, Fullname> newData)
	{
		var ordersToUpdate = OrderDescriptionViewModels
			.Where(x => x.ClientInfo.Id == newData.Key);

		foreach(var order in ordersToUpdate)
		{
			order.ClientInfo.Fullname = newData.Value;
		}
	}

	private void OnChangedClientPhoneNumber(object? sender, KeyValuePair<Guid, string> newData)
	{
		var ordersToUpdate = OrderDescriptionViewModels
			.Where(x => x.ClientInfo.Id == newData.Key);

		foreach(var order in ordersToUpdate)
		{
			order.ClientInfo.PhoneNumber = newData.Value;
		}
	}

	private async void OnCreatedPackageAsync(object? sender, Package newData)
	{
		if(_postmanHelper.PostmanId == newData.PackagePostmanId)
		{
			var clientInfo = new ReadModels.Client();

			try
			{
				clientInfo = (await _clientGraphQlClient.GetClientsByIdsAsync([newData.PackageClientId]))[0];
			}
			catch(GraphQlClientException graphQlEx)
			{
				await DialogServiceInteractions.ShowErrorMessage.Handle(new()
				{
					Title = "GraphQL Error",
					Message = graphQlEx.Message
				});
				Log.Error(graphQlEx, "GraphQL error occurred while initializing orders.");
				return;
			}
			catch(Exception ex)
			{
				await DialogServiceInteractions.ShowErrorMessage.Handle(new()
				{
					Title = "Unexpected Error",
					Message = "An unexpected error occurred while loading orders. Please try again later."
				});
				Log.Error(ex, "Unexpected error occurred while initializing orders.");
				return;
			}

			var orderDescriptionViewModel = await LoadOrderDescriptionAsync(newData, clientInfo);

			if(orderDescriptionViewModel != null)
			{
				OrderDescriptionViewModels.Add(orderDescriptionViewModel);
			}
		}
	}

	private async void OnChangedPackageAsync(object? sender, Package newData)
	{
		var orderToUpdate = OrderDescriptionViewModels
			.FirstOrDefault(x => x.PackageId == newData.Id);

		if(orderToUpdate != default)
		{
			if(_postmanHelper.PostmanId != newData.PackagePostmanId)
			{
				if(orderToUpdate.IsCurrent)
				{
					ClearCurrentOrderEvent?.Invoke(this, newData.Id);
				}
				OrderDescriptionViewModels.Remove(orderToUpdate);
			}
			else
			{
				var newClientInfo = new ReadModels.Client();

				try
				{
					newClientInfo = (await _clientGraphQlClient.GetClientsByIdsAsync([newData.PackageClientId]))[0];
				}
				catch(GraphQlClientException graphQlEx)
				{
					await DialogServiceInteractions.ShowErrorMessage.Handle(new()
					{
						Title = "GraphQL Error",
						Message = graphQlEx.Message
					});
					Log.Error(graphQlEx, "GraphQL error occurred while initializing orders.");
					return;
				}
				catch(Exception ex)
				{
					await DialogServiceInteractions.ShowErrorMessage.Handle(new()
					{
						Title = "Unexpected Error",
						Message = "An unexpected error occurred while loading orders. Please try again later."
					});
					Log.Error(ex, "Unexpected error occurred while initializing orders.");
					return;
				}

				orderToUpdate.ClientInfo = new ClientOfOrderHelper()
				{
					Id = newClientInfo.Id,
					Fullname = newClientInfo.Fullname,
					PhoneNumber = newClientInfo.PhoneNumber
				};
			}
		}
	}

	private async void OnChangedPackageAddress(object? sender, KeyValuePair<Guid, Address> newData)
	{
		var orderToUpdate = OrderDescriptionViewModels.FirstOrDefault(x => x.PackageId == newData.Key);
		
		if(orderToUpdate != default)
		{
			orderToUpdate.Address = await _addressClient.GetAddressAsync(
				newData.Value.Latitude,
				newData.Value.Longitude);
		}
	}

	private void OnChangedPackageDimensions(object? sender, KeyValuePair<Guid, Dimensions> newData)
	{
		var orderToUpdate = OrderDescriptionViewModels.FirstOrDefault(x => x.PackageId == newData.Key);

		if(orderToUpdate != default)
		{
			orderToUpdate.ImagePath = GetOrderImage(newData.Value);
		}
	}

	private void OnChangedPackageStatus(object? sender, KeyValuePair<Guid, PackageStatus> newData)
	{
		var orderToUpdate = OrderDescriptionViewModels.FirstOrDefault(x => x.PackageId == newData.Key);

		if(orderToUpdate != default)
		{
			orderToUpdate.Status = newData.Value;
		}
	}

	private void OnDeletedPackage(object? sender, Guid deletedPackageId)
	{
		var deletedPackage = OrderDescriptionViewModels.FirstOrDefault(x => x.PackageId == deletedPackageId);
		if(deletedPackage != default)
		{
			if(deletedPackage.IsCurrent)
			{
				ClearCurrentOrderEvent?.Invoke(this, deletedPackageId);
			}
			OrderDescriptionViewModels.Remove(deletedPackage);
		}
	}

	#endregion
}
