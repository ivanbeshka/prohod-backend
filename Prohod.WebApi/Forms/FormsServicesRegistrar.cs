using Prohod.Domain.Forms;
using Prohod.Infrastructure.Forms;

namespace Prohod.WebApi.Forms;

public static class FormsServicesRegistrar
{
    public static IServiceCollection AddFormsServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<IFormsRepository, FormsRepository>();
    }
}