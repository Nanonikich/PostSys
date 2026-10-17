using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PostSys.Application.Contracts;

namespace PostSys.Service.Auth;

/// <summary>Реализация выдачи ключей доступа почтальонам.</summary>
/// <param name="options">Параметры выдачи ключей доступа.</param>
public sealed class JwtTokenService(IOptions<JwtOptions> options) : IJwtTokenService
{
    /// <inheritdoc/>
    public AuthResult CreateToken(Guid postmanId, string email)
    {
        var jwt = options.Value;
        var expires = DateTimeOffset.UtcNow.AddMinutes(Math.Clamp(jwt.ExpiryMinutes, 5, 24 * 60));
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SigningKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, postmanId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.NameIdentifier, postmanId.ToString()),
            new Claim("postman_id", postmanId.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwt.Issuer,
            audience: jwt.Audience,
            claims: claims,
            expires: expires.UtcDateTime,
            signingCredentials: credentials);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        return new(postmanId, accessToken, expires);
    }
}