using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using NLog;

using ReactiveUI;

using PostSys.Client;
using PostSys.Client.Data;
using PostSys.Client.GraphQl;
using PostSys.Gui.Common.Services.Dialogs;
using PostSys.Gui.Common.ViewModels;
using PostSys.Gui.Helpers;
using PostSys.Gui.Localization;
using PostSys.Gui.Services.Storage;

namespace PostSys.Gui.ViewModels;

/// <summary>Модель представления экрана входа.</summary>
public class AuthorizationViewModel : ViewModelBase
{
	private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

	private string _email = string.Empty;
	private string _password = string.Empty;
	private bool _rememberMe = true;
	private readonly PostmanHelper _postmanHelper;
	private readonly IPostmanGraphQlClient _postmanGraphQlClient;
	private readonly GraphQlConnection _graphQlConnection;
	private readonly ISecureSessionStore _sessionStore;

	/// <summary>Команда входа.</summary>
	public ICommand AuthorizationCommand { get; }

	/// <summary>Адрес электронной почты.</summary>
	public string Email
	{
		get => _email;
		set => this.RaiseAndSetIfChanged(ref _email, value);
	}

	/// <summary>Пароль.</summary>
	public string Password
	{
		get => _password;
		set => this.RaiseAndSetIfChanged(ref _password, value);
	}

	/// <summary>Признак сохранения сеанса на устройстве.</summary>
	public bool RememberMe
	{
		get => _rememberMe;
		set => this.RaiseAndSetIfChanged(ref _rememberMe, value);
	}

	/// <summary>Взаимодействия с диалоговыми окнами.</summary>
	public DialogServiceInteractions DialogServiceInteractions { get; init; } = new();

	/// <summary>Событие успешного входа.</summary>
	public event EventHandler<bool>? SuccessfulAuthorizationEvent;

	/// <summary>Создаёт экземпляр класса <see cref="AuthorizationViewModel"/>.</summary>
	/// <param name="postmanHelper">Данные текущего почтальона.</param>
	/// <param name="postmanGraphQlClient">Клиент запросов к почтальонам.</param>
	/// <param name="graphQlConnection">Общее подключение к службе.</param>
	/// <param name="sessionStore">Защищённое хранилище сеанса.</param>
	public AuthorizationViewModel(
		PostmanHelper postmanHelper,
		IPostmanGraphQlClient postmanGraphQlClient,
		GraphQlConnection graphQlConnection,
		ISecureSessionStore sessionStore)
	{
		_postmanHelper = postmanHelper;
		_postmanGraphQlClient = postmanGraphQlClient;
		_graphQlConnection = graphQlConnection;
		_sessionStore = sessionStore;

		AuthorizationCommand = ReactiveCommand.CreateFromTask(UserAuthorizationAsync);
		_ = TryRestoreSessionAsync();
	}

	private async Task TryRestoreSessionAsync()
	{
		try
		{
			var credentials = await _sessionStore.LoadCredentialsAsync();
			if(credentials is { } creds)
			{
				Email = creds.Email;
				Password = creds.Password;
			}

			var session = await _sessionStore.LoadAsync();
			if(session is null || session.ExpiresAt <= DateTimeOffset.UtcNow)
				return;

			_postmanHelper.PostmanId = session.PostmanId;
			_postmanHelper.AccessToken = session.AccessToken;
			_postmanHelper.TokenExpiresAt = session.ExpiresAt;
			_postmanHelper.Email = session.Email;
			_graphQlConnection.SetAccessToken(session.AccessToken);
			Email = session.Email;
			SuccessfulAuthorizationEvent?.Invoke(this, true);
		}
		catch(Exception ex)
		{
			Log.Warn(ex, "Failed to restore session.");
		}
	}

	private async Task UserAuthorizationAsync()
	{
		if(string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
		{
			await DialogServiceInteractions.ShowErrorMessage.Handle(new()
			{
				Title = UiLocalizer.Get("Title_ValidationError"),
				Message = UiLocalizer.Get("Msg_EmailPasswordRequired")
			});
			return;
		}

		try
		{
			var auth = await _postmanGraphQlClient.AuthorizeAsync(Email, Password);
			_postmanHelper.PostmanId = auth.PostmanId;
			_postmanHelper.AccessToken = auth.AccessToken;
			_postmanHelper.TokenExpiresAt = auth.ExpiresAt;
			_postmanHelper.Email = Email;
			_graphQlConnection.SetAccessToken(auth.AccessToken);

			if(RememberMe)
			{
				await _sessionStore.SaveAsync(
					new SecureSession(Email, auth.AccessToken, auth.PostmanId, auth.ExpiresAt, null),
					Password);
			}
			else
			{
				await _sessionStore.ClearAsync();
			}

			SuccessfulAuthorizationEvent?.Invoke(this, true);
			Log.Info("User {Email} authorized ({PostmanId}).", Email, auth.PostmanId);
		}
		catch(GraphQlClientException postmanEx)
		{
			await DialogServiceInteractions.ShowErrorMessage.Handle(new()
			{
				Title = UiLocalizer.Get("Title_AuthorizationError"),
				Message = postmanEx.Message
			});
			Log.Error(postmanEx, "Authorization failed for {Email}.", Email);
		}
		catch(Exception ex)
		{
			await DialogServiceInteractions.ShowErrorMessage.Handle(new()
			{
				Title = UiLocalizer.Get("Title_Error"),
				Message = UiLocalizer.Get("Msg_UnexpectedTryLater")
			});
			Log.Error(ex, "Unexpected authorization error for {Email}.", Email);
		}
	}
}
