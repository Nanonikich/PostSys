using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using NLog;

using ReactiveUI;

using PostSys.Client;
using PostSys.Client.Data;
using PostSys.Gui.Common.Services.Dialogs;
using PostSys.Gui.Common.ViewModels;
using PostSys.Gui.Helpers;

namespace PostSys.Gui.ViewModels;

/// <summary>Модель представления авторизации.</summary>
public class AuthorizationViewModel : ViewModelBase
{
	private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

	private string _email = string.Empty;
	private string _password = string.Empty;
	private readonly PostmanHelper _postmanHelper;
	private readonly IPostmanGraphQlClient _postmanGraphQlClient;

	/// <summary>Возвращает команду для авторизации.</summary>
	public ICommand AuthorizationCommand { get; }

	/// <summary>Возвращает или задаёт Email пользователя.</summary>
	public string Email
	{
		get => _email;
		set => this.RaiseAndSetIfChanged(ref _email, value);
	}

	/// <summary>Возвращает или задаёт пароль пользователя.</summary>
	public string Password
	{
		get => _password;
		set => this.RaiseAndSetIfChanged(ref _password, value);
	}

	/// <summary>Возвращает взаимодействие с диалоговыми окнами.</summary>
	public DialogServiceInteractions DialogServiceInteractions { get; init; } = new();

	/// <summary>Событие успешной авторизации.</summary>
	public event EventHandler<bool>? SuccessfulAuthorizationEvent;

	/// <summary>Создаёт экземпляр класса <see cref="AuthorizationViewModel"/>.</summary>
	public AuthorizationViewModel(
		PostmanHelper postmanHelper,
		IPostmanGraphQlClient postmanGraphQlClient)
	{
		_postmanHelper = postmanHelper;
		_postmanGraphQlClient = postmanGraphQlClient;

		AuthorizationCommand = ReactiveCommand.CreateFromTask(UserAuthorizationAsync);
	}

	private async Task UserAuthorizationAsync()
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
			Log.Info("User {Email} successfully authorized with ID {PostmanId}.", Email, _postmanHelper.PostmanId);
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
}
