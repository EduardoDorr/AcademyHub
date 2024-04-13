using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using AcademyHub.Common.Auth;
using AcademyHub.Common.Options;


namespace AcademyHub.Infrastructure.Auth;

public class AuthService : IAuthService
{
    private readonly AuthenticationOptions _authenticationOptions;

    public AuthService(IOptions<AuthenticationOptions> options)
    {
        _authenticationOptions = options.Value;
    }

    public string ComputeSha256Hash(string password) =>
        PasswordHashGenerator.ComputeSha256Hash(password);

    public string GenerateJwtToken(string email, string role)
    {
        var key = _authenticationOptions.Key;
        var issuer = _authenticationOptions.Issuer;
        var audience = _authenticationOptions.Audience;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new("userName", email),
            new(ClaimTypes.Role, role)
        };

        var token =
            new JwtSecurityToken(issuer: issuer,
                                 audience: audience,
                                 expires: DateTime.Now.AddHours(1),
                                 signingCredentials: credentials,
                                 claims: claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }
}