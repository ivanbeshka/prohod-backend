using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Prohod.Domain.Users;
using Prohod.Infrastructure.Accounts.Options;

namespace Prohod.Infrastructure.Accounts.JwtTokens;

public class JwtTokensGenerator : IJwtTokensGenerator
{
    private readonly IOptions<AuthenticationOptions> authenticationOptions;

    public JwtTokensGenerator(IOptions<AuthenticationOptions> authenticationOptions)
    {
        this.authenticationOptions = authenticationOptions;
    }

    public JwtToken GenerateJwtToken(User user)
    {
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(authenticationOptions.Value.SigningKey)), SecurityAlgorithms.HmacSha256);

        var claims = new Claim[]
        {
            new(ClaimTypes.Role, user.Role.ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        var token = new JwtSecurityToken(
            authenticationOptions.Value.Issuer,
            authenticationOptions.Value.Audience,
            claims: claims,
            expires: DateTimeOffset.Now.AddDays(14).DateTime,
            signingCredentials: credentials);

        var writtenToken = new JwtSecurityTokenHandler().WriteToken(token);
        
        return new(writtenToken);
    }
}