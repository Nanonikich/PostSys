using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace PostSys.Service.Extensions;

/// <summary>Определяет расширения для проверки авторизации.</summary>
public static class JwtExtensions
{
	#region Methods

	/// <summary>Регистрирует зависимости для проверки авторизации.</summary>
	/// <param name="services"><see cref="IServiceCollection"/>.</param>
	/// <returns><see cref="IServiceCollection"/>.</returns>
	public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
	{
		services
			.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.Authority = "https://localhost:8080/auth/realms/postSys-realm";
				options.Audience = "postSys-client";
				options.RequireHttpsMetadata = false;
			});
		services.AddAuthorization(options =>
		{
			options.AddPolicy("YourPolicy", policy => policy.RequireAuthenticatedUser());
		});

		return services;
	}

	#endregion
}
