using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Prohod.Infrastructure.Accounts.Options.Configuration;

public class ConfigureAuthenticationOptions : IConfigureOptions<AuthenticationOptions>
{
    private const string SettingsSectionName = "Authentication";
    private readonly IConfiguration configuration;

    public ConfigureAuthenticationOptions(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void Configure(AuthenticationOptions options)
    {
        configuration.GetRequiredSection(SettingsSectionName).Bind(options);
    }
}