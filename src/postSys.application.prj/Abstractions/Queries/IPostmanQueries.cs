namespace PostSys.Application.Abstractions.Queries;

/// <summary>Порт запросов почтальонов.</summary>
public interface IPostmanQueries
{
	/// <summary>Возвращает почтальона по идентификатору.</summary>
	/// <param name="id">Идентификатор почтальона.</param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
	Task<PostSys.ReadModels.Postman?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

	/// <summary>Возвращает запрос для чтения почтальонов.</summary>
	IQueryable<PostSys.ReadModels.Postman> Query();

	/// <summary>Проверяет учётные данные почтальона и возвращает идентификатор при успехе.</summary>
	/// <param name="email">Email.</param>
	/// <param name="password">Пароль в открытом виде.</param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
	Task<Guid?> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default);
}
