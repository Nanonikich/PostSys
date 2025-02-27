using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate.AspNetCore;
using HotChocolate.Execution;

using Microsoft.AspNetCore.Http;

namespace PostSys.Service.Common.GraphQL;

/// <summary>Вводит защиту по ролям для запросов.</summary>
public class HttpRequestInterceptor : DefaultHttpRequestInterceptor
{
	/// <inheritdoc/>
	public override ValueTask OnCreateAsync(
		HttpContext context,
		IRequestExecutor requestExecutor,
		OperationRequestBuilder requestBuilder,
		CancellationToken cancellationToken)
	{
		var identity = new ClaimsIdentity();
		var rolesv = context.User.FindFirstValue("realm_access");
		if(rolesv != null)
		{
			var roles = JsonSerializer.Deserialize<JsonElement>(rolesv);
			if(roles.TryGetProperty("roles", out var rolesArray))
			{
				foreach(var r in rolesArray.EnumerateArray())
				{
					identity.AddClaim(new Claim(ClaimTypes.Role, r.GetString()!));
				}
			}
		}

		var namev = context.User.FindFirstValue("preferred_username");
		if(namev != null)
		{
			identity.AddClaim(new Claim(ClaimTypes.Name, namev));
		}

		context.User.AddIdentity(identity);

		return base.OnCreateAsync(
			context,
			requestExecutor,
			requestBuilder,
			cancellationToken);
	}
}
