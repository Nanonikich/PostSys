using System;
using System.Reactive.Linq;
using System.Windows.Input;
using System.Threading.Tasks;

using NLog;

using ReactiveUI;

using PostSys.Client;
using PostSys.Client.Data;
using PostSys.Gui.Common.Services.Dialogs;
using PostSys.Gui.Common.ViewModels;
using PostSys.Gui.Configuration;
using PostSys.Gui.Helpers;

namespace PostSys.Gui.ViewModels;

/// <summary>Модель представления авторизации.</summary>
public class AuthorizationViewModel : ViewModelBase
{
	#region Static

	private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

	#endregion

	#region Data

	private string _email;
	private string _password;
	private readonly PostmanHelper _postmanHelper;
	private readonly IPostmanGraphQlClient _postmanGraphQlClient;

	#endregion

	#region Properties

	#region Commands

	/// <summary>Возвращает команду для авторизации.</summary>
	/// <value>Команда для авторизации.</value>
	public ICommand AuthorizationCommand { get; }

	#endregion

	/// <summary>Возвращает или задаёт Email пользователя.</summary>
	/// <value>Email пользователя.</value>
	public string Email
	{
		get => _email;
		set => this.RaiseAndSetIfChanged(ref _email, value);
	}

	/// <summary>Возвращает или задаёт пароль пользователя.</summary>
	/// <value>Пароль пользователя.</value>
	public string Password
	{
		get => _password;
		set => this.RaiseAndSetIfChanged(ref _password, value);
	}

	/// <summary>Возвращает взаимодействие с диалоговыми окнами.</summary>
	/// <value>Взаимодействие с диалоговыми окнами.</value>
	public DialogServiceInteractions DialogServiceInteractions { get; init; } = new();

	#endregion

	#region Events

	/// <summary>Событие успешной авторизации.</summary>
	public event EventHandler<bool> SuccessfulAuthorizationEvent;

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="AuthorizationViewModel"/>.</summary>
	/// <param name="postmanHelper">Данные об авторизованном почтальоне.</param>
	/// <param name="postmanGraphQlClient">Клиент GraphQL для сущности "Почтальон".</param>
	/// <param name="appConfiguration">Конфигурация приложения.</param>
	public AuthorizationViewModel(
		PostmanHelper postmanHelper,
		IPostmanGraphQlClient postmanGraphQlClient,
		ApplicationConfiguration appConfiguration)
	{		
		_postmanHelper = postmanHelper;
		_postmanGraphQlClient = postmanGraphQlClient;

		AuthorizationCommand = ReactiveCommand.CreateFromTask(async () => await UserAuthorizationAsync(appConfiguration.ServiceAddress));
	}

	#endregion

	#region Methods

	/// <summary>Выполняет авторизацию пользователя.</summary>
	/// <param name="serviceAddress">Адрес сервиса.</param>
	private async Task UserAuthorizationAsync(string serviceAddress)
	{
		if(string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
		{
			await DialogServiceInteractions.ShowErrorMessage.Handle(new()
			{
				Title = "Validation Error",
				Message = "Email and Password must not be empty."
			});
			Log.Warn("Authorization attempted with empty Email or Password.");
			return;
		}

		try
		{
			_postmanHelper.PostmanId = await _postmanGraphQlClient.GetAuthorizationPostmanIdAsync(Email, Password);
			SuccessfulAuthorizationEvent?.Invoke(this, true);
			Log.Info($"User {Email} successfully authorized with ID {_postmanHelper.PostmanId}.");
		}
		catch(GraphQlClientException postmanEx)
		{
			await DialogServiceInteractions.ShowErrorMessage.Handle(new()
			{
				Title = "Authorization Error",
				Message = postmanEx.Message
			});
			Log.Error(postmanEx, "Error during authorization for user {Email}.", Email);
		}
		catch(Exception ex)
		{
			await DialogServiceInteractions.ShowErrorMessage.Handle(new()
			{
				Title = "Error",
				Message = "An unexpected error occurred. Please try again later."
			});
			Log.Error(ex, "Unexpected error during authorization for user {Email}.", Email);
		}
	}

	#endregion
}
