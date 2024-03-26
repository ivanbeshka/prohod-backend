using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Prohod.Infrastructure.Accounts.Passwords.Options.Configuration;

public class ConfigurePasswordsSaltOptions : IConfigureOptions<PasswordsSalt>
{
    private readonly IConfiguration configuration;

    public ConfigurePasswordsSaltOptions(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void Configure(PasswordsSalt options)
    {
        configuration.Bind(nameof(PasswordsSalt), options);
    }
}