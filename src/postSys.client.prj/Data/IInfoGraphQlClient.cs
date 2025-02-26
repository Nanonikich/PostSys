using System.Threading;
using System.Threading.Tasks;

namespace PostSys.Client.Data;

/// <summary>Клиент GraphQL для получения информации о сервисе.</summary>
public interface IInfoGraphQlClient
{
	/// <summary>Получает информацию о версии сервиса.</summary>
	/// <param name="cancellationToken">Токен отмены.</param>
	/// <returns>Асинхронная операция, возвращающая версию сервиса.</returns>
	Task<string> GetVersionAsync(CancellationToken cancellationToken = default);
}
