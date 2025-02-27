using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using PostSys.IdentityService.Data;

namespace PostSys.IdentityService;

/// <summary>Обработчик управления токеном авторизации.</summary>
/// <param name="tokenProvider">Провайдер токена авторизации KeyCloak.</param>
public class TokenDelegatingHandler(ITokenProvider tokenProvider) : DelegatingHandler
{
	#region Handlers

	/// <inheritdoc/>
	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		string text = await tokenProvider.GetToken();
		if(!string.IsNullOrWhiteSpace(text))
		{
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", text);
		}

		return await base.SendAsync(request, cancellationToken);
	}

	#endregion
}