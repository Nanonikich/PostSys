using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using PostSys.IdentityService.Data;

namespace PostSys.IdentityService;

/// <summary>Добавляет токен авторизации в заголовки запросов HotChocolate.</summary>
public class TokenMiddleware
{
	#region Data

	private readonly RequestDelegate _next;
	private readonly ITokenProvider _tokenProvider;

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="TokenMiddleware"/>.</summary>
	/// <param name="next">Делегат </param>
	/// <param name="tokenProvider"><see cref="ITokenProvider"/>.</param>
	public TokenMiddleware(RequestDelegate next, ITokenProvider tokenProvider)
	{
		_next = next;
		_tokenProvider = tokenProvider;
	}

	#endregion

	#region Methods

	/// <summary>Передаёт значение делегату.</summary>
	/// <param name="context">Контекст Http.</param>
	/// <returns>Асинхронная операция.</returns>
	public async Task InvokeAsync(HttpContext context)
	{
		var token = await _tokenProvider.GetToken();

		if(!string.IsNullOrEmpty(token))
		{
			context.Request.Headers["Authorization"] = $"Bearer {token}";
		}

		await _next(context);
	}

	#endregion
}