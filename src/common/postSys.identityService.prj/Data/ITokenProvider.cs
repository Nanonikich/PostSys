using System.Threading.Tasks;

namespace PostSys.IdentityService.Data;

/// <summary>Провайдер токена авторизации.</summary>
public interface ITokenProvider
{
	#region Methods

	/// <summary>Получает токен авторизации.</summary>
	/// <returns>Токен авторизации.</returns>
	ValueTask<string> GetToken();

	#endregion
}
