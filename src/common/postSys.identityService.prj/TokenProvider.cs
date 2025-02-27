using System;
using System.Threading.Tasks;

using NLog;

using PostSys.IdentityService.Data;

namespace PostSys.IdentityService;

/// <summary>Провайдер токена авторизации KeyCloak.</summary>
/// <param name="authorizationToken">Токен авторизации.</param>
public class TokenProvider(string authorizationToken) : ITokenProvider
{
	#region Statics

	private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

	#endregion

	#region Methods

	/// <inheritdoc/>
	public ValueTask<string> GetToken()
	{
		if(!string.IsNullOrWhiteSpace(authorizationToken))
		{
			return new ValueTask<string>(authorizationToken);
		}

		Log.Error("Failed to get the KeyCloak authorization token.");
		throw new InvalidOperationException("Failed to get the KeyCloak authorization token.");
	}

	#endregion
}
