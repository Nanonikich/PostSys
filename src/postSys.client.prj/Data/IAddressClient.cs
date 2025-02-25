using System.Threading;
using System.Threading.Tasks;

namespace PostSys.Client.Data;

/// <summary>Клиент получения адреса через Яндекс.API по координатам.</summary>
public interface IAddressClient
{
	#region Methods

	/// <summary>Получает конкретный адрес с помощью Яндекс.API по координатам.</summary>
	/// <param name="latitude">Адрес - широта.</param>
	/// <param name="longitude">Адрес - долгота.</param>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Асинхронная операция, возвращающая конкретный адрес по координатам.</returns>
	Task<string> GetAddressAsync(double latitude, double longitude, CancellationToken cancellationToken = default);

	#endregion
}
