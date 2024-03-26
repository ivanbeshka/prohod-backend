using Microsoft.EntityFrameworkCore;
using Npgsql;
using Prohod.Domain.Users;
using Prohod.Domain.VisitRequests;
using Prohod.Infrastructure.Database;

namespace Prohod.WebApi.Configuration;

public static class PostgresDbContextRegistrar
{
    private const string PostgresConnectionStringName = "PostgreSql";

    public static IServiceCollection AddPostgresDbContext(
        this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var npgsqlDataSourceBuilder = 
            new NpgsqlDataSourceBuilder(configuration.GetConnectionString(PostgresConnectionStringName));
        npgsqlDataSourceBuilder.MapEnum<VisitRequestStatus>();
        npgsqlDataSourceBuilder.MapEnum<Role>();

        return serviceCollection
            .AddDbContext<PostgresDbContext>(options => 
                options
                    .UseNpgsql(npgsqlDataSourceBuilder.Build())
                    .UseLazyLoadingProxies()
                )
            .AddScoped<IAppDbContext, PostgresDbContext>();
    }
}