using Domain.Models;
using Domain.Options;
using Domain.Responses;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service;

public interface IJwtService
{
    JwtResponse CreateToken(User user);
    JwtSecurityToken ValidateToken(string jwt);
}

public class JwtService : IJwtService
{
    private readonly TokenOptions _options;
    public readonly SigningCredentials _credentials;

    public JwtService(IOptions<TokenOptions> options)
    {
        _options = options.Value;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key!));
        _credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
    }

    public JwtResponse CreateToken(User user)
    {
        var identity = CreateClaimsIdentity(user);

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            IssuedAt = _options.IssuedAt,
            NotBefore = _options.NotBefore,
            Expires = _options.AccessTokenExpiration,
            SigningCredentials = _credentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new JwtResponse
        {
            Token = tokenHandler.WriteToken(token),
            Expiration = token.ValidTo
        };
    }

    public JwtSecurityToken ValidateToken(string token)
    {
        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.FromMinutes(5),
                IssuerSigningKey = _credentials.Key,
                RequireSignedTokens = true,
                RequireExpirationTime = true,
                ValidateLifetime = false,
                ValidateAudience = true,
                ValidAudience = _options.Audience,
                ValidateIssuer = true,
                ValidIssuer = _options.Issuer
            };

            var handler = new JwtSecurityTokenHandler();
            var claimsPrincipal = handler.ValidateToken(token, validationParameters, out var rawValidatedToken);

            return (JwtSecurityToken)rawValidatedToken;
        }
        catch
        {
            throw new SecurityTokenValidationException("Invalid token");
        }
    }

    private ClaimsIdentity CreateClaimsIdentity(User user)
    {
        var userDataClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
        };
        return new ClaimsIdentity(userDataClaims, "Custom");
    }
}
