using Prohod.Domain.Users;
using Prohod.Infrastructure.Users;

namespace Prohod.WebApi.Users;

public static class UsersServicesRegistrar
{
    public static IServiceCollection AddUsersServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<IUsersRepository, UsersRepository>();
    }
}
