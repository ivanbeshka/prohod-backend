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
        var connectionString = configuration.GetConnectionString(PostgresConnectionStringName);
        var npgsqlDataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        npgsqlDataSourceBuilder.MapEnum<VisitRequestStatus>();
        npgsqlDataSourceBuilder.MapEnum<Role>();
        var dataSource = npgsqlDataSourceBuilder.Build();
        return serviceCollection
            .AddDbContext<PostgresDbContext>(options => 
                options
                    .UseNpgsql(dataSource)
                    .UseLazyLoadingProxies()
                )
            .AddScoped<IAppDbContext, PostgresDbContext>();
    }
}