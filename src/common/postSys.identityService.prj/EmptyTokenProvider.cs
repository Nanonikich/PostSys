using System.Threading.Tasks;

using PostSys.IdentityService.Data;

namespace PostSys.IdentityService;

/// <summary>Провайдер пустого токена авторизации.</summary>
public class EmptyTokenProvider : ITokenProvider
{
	#region Methods

	/// <inheritdoc/>
	public ValueTask<string> GetToken()
	{
		return new ValueTask<string>(string.Empty);
	}

	#endregion
}