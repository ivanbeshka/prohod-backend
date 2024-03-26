using Microsoft.AspNetCore.Authentication.JwtBearer;
using Prohod.Infrastructure.Accounts;
using Prohod.Infrastructure.Accounts.JwtTokens;
using Prohod.Infrastructure.Accounts.Options.Configuration;
using Prohod.Infrastructure.Accounts.Passwords;
using Prohod.Infrastructure.Accounts.Passwords.Options;
using Prohod.Infrastructure.Accounts.Passwords.Options.Configuration;
using Prohod.Infrastructure.Accounts.Repository;
using AuthenticationOptions = Prohod.Infrastructure.Accounts.Options.AuthenticationOptions;

namespace Prohod.WebApi.Accounts.Configuration;

public static class AccountsRegistrar
{
    public static IServiceCollection AddAccountsServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<PasswordsSalt>();
        serviceCollection.AddOptions<AuthenticationOptions>();
        serviceCollection.ConfigureOptions<ConfigureAuthenticationOptions>();
        serviceCollection.ConfigureOptions<ConfigurePasswordsSaltOptions>();
        serviceCollection.ConfigureOptions<ConfigureJwtBearerOptions>();
        serviceCollection
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        
        return serviceCollection
            .AddScoped<IJwtTokensGenerator, JwtTokensGenerator>()
            .AddScoped<IAccountsRepository, AccountsRepository>()
            .AddScoped<IAccountsService, AccountsService>()
            .AddScoped<IPasswordsHashCalculator, Sha256PasswordsHashCalculator>();
    }
}