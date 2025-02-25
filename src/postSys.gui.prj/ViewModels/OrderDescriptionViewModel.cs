using System;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Windows.Input;

using NLog;

using ReactiveUI;

using PostSys.Client;
using PostSys.Client.Data;
using PostSys.Gui.Common.Helpers;
using PostSys.Gui.Common.ViewModels;
using PostSys.Gui.Configuration;
using PostSys.Gui.Helpers;
using PostSys.ReadModels.Contracts;

namespace PostSys.Gui.ViewModels;

/// <summary>Модель представления описания посылки.</summary>
public class OrderDescriptionViewModel : ViewModelBase, IActivatableViewModel
{
	#region Static

	private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

	#endregion

	#region Data

	public string _imagePath;
	public string _address;
	public ClientOfOrderHelper _clientInfo;
	private Guid _packageId;
	private PackageStatus _status;
	private readonly IPackageGraphQlClient _packageGraphQlClient;

	#endregion

	#region Properties

	#region Commands

	/// <summary>Возвращает команду кнопки выбора заказа.</summary>
	/// <value>Команда кнопки выбора заказа.</value>
	public ICommand SelectOrderCommand { get; }

	#endregion

	/// <inheritdoc/>
	public ViewModelActivator Activator { get; }

	/// <summary>Возвращает или задаёт флаг выбранной почтальоном посылки.</summary>
	/// <value>Флаг выбранной почтальоном посылки.</value>
	public bool IsCurrent { get; private set; }

	/// <summary>Возвращает или задаёт идентификатор посылки.</summary>
	/// <value>Идентификатор посылки.</value>
	public Guid PackageId
	{
		get => _packageId;
		set => this.RaiseAndSetIfChanged(ref _packageId, value);
	}

	/// <summary>Возвращает или задаёт путь к изображению посылки.</summary>
	/// <value>Путь к изображению посылки.</value>
	public string ImagePath
	{
		get => _imagePath;
		set => this.RaiseAndSetIfChanged(ref _imagePath, value);
	}

	/// <summary>Возвращает или задаёт адрес, по которому находится клиент.</summary>
	/// <value>Адрес, по которому находится клиент.</value>
	public string Address
	{
		get => _address;
		set => this.RaiseAndSetIfChanged(ref _address, value);
	}

	/// <summary>Возвращает или задаёт адрес, идентификатор клиента.</summary>
	/// <value>Идентификатор клиента.</value>
	public ClientOfOrderHelper ClientInfo
	{
		get => _clientInfo;
		set => this.RaiseAndSetIfChanged(ref _clientInfo, value);
	}

	/// <summary>Возвращает или задаёт статус заказа.</summary>
	/// <value>Статус заказа.</value>
	public PackageStatus Status
	{
		get => _status;
		set => this.RaiseAndSetIfChanged(ref _status, value);
	}

	/// <summary>Возвращает или задаёт уведомление об ошибке.</summary>
	/// <value>Уведомление об ошибке.</value>
	public Interaction<MessageContent, bool> ShowErrorMessage { get; set; } = new();

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="OrderDescriptionViewModel"/>.</summary>
	/// <param name="postmanHelper">Данные об авторизованном почтальоне.</param>
	/// <param name="postmanGraphQlClient">Клиент GraphQL для сущности "Почтальон".</param>
	/// <param name="packageGraphQlClient">Клиент GraphQL для сущности "Посылка".</param>
	/// <param name="appConfiguration">Конфигурация приложения.</param>
	public OrderDescriptionViewModel(
		PostmanHelper postmanHelper,
		IPostmanGraphQlClient postmanGraphQlClient,
		IPackageGraphQlClient packageGraphQlClient,
		ApplicationConfiguration appConfiguration)
	{
		Activator = new();

		_packageGraphQlClient = packageGraphQlClient;
		SelectOrderCommand = ReactiveCommand.CreateFromTask(() => 
			SetOrderOnPostmanAsync(
				postmanHelper.PostmanId,
				postmanGraphQlClient,
				appConfiguration.ServiceAddress));
	}

	#endregion

	#region Methods

	private async Task SetOrderOnPostmanAsync(
		Guid postmanId,
		IPostmanGraphQlClient postmanGraphQlClient,
		string serviceAddress)
	{
		try
		{
			var result = await postmanGraphQlClient.ChangePostmanPackageAsync(postmanId, PackageId);

			if(!result)
			{
				await ShowErrorMessage.Handle(new()
				{
					Title = "Order Change Failed",
					Message = "Unable to change the order for the specified postman. Please try again."
				});
				Log.Warn($"Failed to change order for Postman ID {postmanId}.");
				return;
			}

			await ChangePackageStatusAsync(serviceAddress);
		}
		catch(GraphQlClientException graphQlEx)
		{
			await ShowErrorMessage.Handle(new()
			{
				Title = "GraphQL Error",
				Message = graphQlEx.Message
			});
			Log.Error(graphQlEx, "Error while changing order for Postman ID {PostmanId}.", postmanId);
		}
		catch(Exception ex)
		{
			await ShowErrorMessage.Handle(new()
			{
				Title = "Unexpected Error",
				Message = "An unexpected error occurred while changing the order. Please try again later."
			});
			Log.Error(ex, "Unexpected error while changing order for Postman ID {PostmanId}.", postmanId);
		}
	}

	private async Task ChangePackageStatusAsync(string serviceAddress)
	{
		try
		{
			var result = await _packageGraphQlClient.ChangePackageStatusAsync(PackageId, PackageStatus.InWork);

			if(!result)
			{
				Log.Warn($"Failed to change package status for Package ID {PackageId}.");
				await ShowErrorMessage.Handle(new()
				{
					Title = "Status Change Failed",
					Message = "Unable to change the package status. Please try again."
				});
			}
			else
			{
				Status = PackageStatus.InWork;
				IsCurrent = true;
			}
		}
		catch(Exception ex)
		{
			await ShowErrorMessage.Handle(new()
			{
				Title = "Status Change Error",
				Message = "An error occurred while changing the package status. Please try again later."
			});
			Log.Error(ex, "Error while changing package status for Package ID {PackageId}.", PackageId);
		}
	}

	#endregion
}
