using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using NLog;

using PostSys.Framework.Common;
using PostSys.Framework.Common.Data;
using PostSys.IdentityService.Contracts;
using PostSys.IdentityService.Data;

namespace PostSys.IdentityService;

/// <summary>Провайдер токена KeyCloak.</summary>
public class KeyCloakTokenProvider : ITokenProvider, IDisposable
{
	#region Statics

	private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

	#endregion

	#region Data

	private string _authorizationToken = "";
	private string _refreshToken = "";
	private readonly Dictionary<string, string> _tokenParameters;
	private readonly HttpClient _httpClient;
	private readonly KeyCloakAuthorizationData _keyCloakAuthorizationData;
	private readonly SemaphoreLocker _tokenSemaphore = new();
	private readonly IScheduler _scheduler = new Scheduler();

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="KeyCloakTokenProvider"/>.</summary>
	/// <param name="keyCloakAuthorizationData">Данные для авторизации в KeyCloak.</param>
	public KeyCloakTokenProvider(KeyCloakAuthorizationData keyCloakAuthorizationData)
	{
		_keyCloakAuthorizationData = keyCloakAuthorizationData;
		_httpClient = new HttpClient();
		_tokenParameters = new Dictionary<string, string>
		{
			{ "client_id", _keyCloakAuthorizationData.ClientId },
			{ "client_secret", _keyCloakAuthorizationData.ClientSecret },
			{ "grant_type", "client_credentials" }
		};
	}


	/// <summary>Создаёт экземпляр класса <see cref="KeyCloakTokenProvider"/>.</summary>
	/// <param name="keyCloakAuthorizationData">Данные для авторизации в KeyCloak.</param>
	/// <param name="httpClientFactory">Фабрика клиента Http.</param>
	public KeyCloakTokenProvider(KeyCloakAuthorizationData keyCloakAuthorizationData, IHttpClientFactory httpClientFactory)
	{
		_keyCloakAuthorizationData = keyCloakAuthorizationData;
		_httpClient = httpClientFactory.CreateClient();
		_tokenParameters = new Dictionary<string, string>
		{
			{ "client_id", _keyCloakAuthorizationData.ClientId },
			{ "client_secret", _keyCloakAuthorizationData.ClientSecret },
			{ "grant_type", "client_credentials" }
		};
	}

	#endregion

	#region Methods

	/// <inheritdoc/>
	public async ValueTask<string> GetToken()
	{
		return await _tokenSemaphore.LockAsync(async delegate
		{
			if(string.IsNullOrEmpty(_authorizationToken))
			{
				await Initialize();
			}

			return _authorizationToken;
		});
	}

	private async Task Initialize()
	{
		try
		{
			OpenIdLoginResponse openIdLoginResponse = await RequestToken(_tokenParameters);
			_authorizationToken = openIdLoginResponse.AccessToken;
			_refreshToken = openIdLoginResponse.RefreshToken;
			ScheduleTokenRefresh(openIdLoginResponse.ExpiresIn);
		}
		catch(Exception exception)
		{
			Log.Error(exception, "Failed receive token");
		}
	}

	private void ScheduleTokenRefresh(int expiresIn)
	{
		DateTime executeAt = DateTime.UtcNow.Add(TimeSpan.FromSeconds(expiresIn / 2));
		_scheduler.TrySchedule(executeAt, delegate
		{
			Task.Run(async delegate
			{
				await GetTokenFromKeyCloak();
			});
		});
	}

	private async Task GetTokenFromKeyCloak()
	{
		await _tokenSemaphore.LockAsync(async delegate
		{
			try
			{
				await RefreshTokenUsingRefreshToken();
			}
			catch(Exception)
			{
				await Initialize();
			}
		});
	}

	private async Task RefreshTokenUsingRefreshToken()
	{
		var openIdLoginResponse = await RequestToken(new Dictionary<string, string>
		{
			{ "grant_type", "refresh_token" },
			{ "client_id", _keyCloakAuthorizationData.ClientId },
			{ "client_secret", _keyCloakAuthorizationData.ClientSecret },
			{ "refresh_token", _refreshToken }
		});
		_authorizationToken = openIdLoginResponse.AccessToken;
		_refreshToken = openIdLoginResponse.RefreshToken;
		ScheduleTokenRefresh(openIdLoginResponse.RefreshExpiresIn);
	}

	private async Task<OpenIdLoginResponse> RequestToken(Dictionary<string, string> parameters)
	{
		string requestUri = _keyCloakAuthorizationData.KeyCloakUrl + "/realms/" + _keyCloakAuthorizationData.Realm + "/protocol/openid-connect/token";
		FormUrlEncodedContent content = new(parameters);
		var httpResponseMessage = await _httpClient.PostAsync(requestUri, content);
		if(httpResponseMessage.IsSuccessStatusCode)
		{
			return await httpResponseMessage.Content.ReadFromJsonAsync<OpenIdLoginResponse>();
		}

		throw new InvalidOperationException($"Error receiving token, response code: {httpResponseMessage.StatusCode}");
	}

	#endregion

	#region IDisposable

	/// <summary>Возвращает или задаёт флаг уничтожены ли зависимости.</summary>
	/// <value>Флаг уничтожены ли зависимости.</value>
	public bool IsDisposed { get; private set; }

	/// <inheritdoc/>
	public void Dispose()
	{
		if(!IsDisposed)
		{
			IsDisposed = true;
			_scheduler.Dispose();
			_httpClient.Dispose();
		}
	}

	#endregion
}
