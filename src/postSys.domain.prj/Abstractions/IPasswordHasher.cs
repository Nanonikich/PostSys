namespace PostSys.Domain.Abstractions;

/// <summary>Порт хеширования паролей.</summary>
public interface IPasswordHasher
{
	/// <summary>Хеширует пароль в открытом виде.</summary>
	/// <param name="password">Пароль в открытом виде.</param>
	/// <returns>Хеш пароля.</returns>
	string Hash(string password);

	/// <summary>Проверяет соответствие пароля хешу.</summary>
	/// <param name="hashedPassword">Хеш пароля.</param>
	/// <param name="providedPassword">Пароль в открытом виде.</param>
	/// <returns><see langword="true"/>, если пароль совпадает.</returns>
	bool Verify(string hashedPassword, string providedPassword);
}
