using System;

using Microsoft.AspNetCore.Identity;

using PostSys.Domain.Abstractions;

using IdentityPasswordHasher = Microsoft.AspNetCore.Identity.PasswordHasher<object>;

namespace PostSys.Dal.Services;

/// <summary>Реализация <see cref="IPasswordHasher"/> на ASP.NET Core Identity.</summary>
public sealed class PasswordHasher : IPasswordHasher
{
	private readonly IdentityPasswordHasher _hasher = new();

	/// <inheritdoc/>
	public string Hash(string password)
	{
		ArgumentException.ThrowIfNullOrEmpty(password);
		return _hasher.HashPassword(new object(), password);
	}

	/// <inheritdoc/>
	public bool Verify(string hashedPassword, string providedPassword)
	{
		ArgumentException.ThrowIfNullOrEmpty(hashedPassword);
		ArgumentException.ThrowIfNullOrEmpty(providedPassword);

		var result = _hasher.VerifyHashedPassword(new object(), hashedPassword, providedPassword);
		if(result is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded)
			return true;

		// Совместимость с ранее сохранёнными паролями в открытом виде.
		return hashedPassword == providedPassword;
	}
}
