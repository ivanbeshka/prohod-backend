using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Prohod.Infrastructure.Accounts.Options;

namespace Prohod.WebApi.Accounts.Configuration;

internal class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
{
    private const string DefaultJwtBearerOptionsName = "Bearer";
    private readonly IOptions<AuthenticationOptions> authenticationOptions;

    public ConfigureJwtBearerOptions(IOptions<AuthenticationOptions> authenticationOptions)
    {
        this.authenticationOptions = authenticationOptions;
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        if (name is not null and not DefaultJwtBearerOptionsName)
        {
            return;
        }
        
        Configure(options);
    }

    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidIssuer = authenticationOptions.Value.Issuer,
            ValidAudience = authenticationOptions.Value.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(authenticationOptions.Value.SigningKey)),
            ValidateIssuerSigningKey = true,
        };
    }
}