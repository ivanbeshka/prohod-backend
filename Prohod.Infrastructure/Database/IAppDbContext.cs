using Microsoft.EntityFrameworkCore;
using Prohod.Domain.AggregationRoot;

namespace Prohod.Infrastructure.Database;

public interface IAppDbContext
{
    DbSet<T> Set<T>()
        where T : class, IAggregationRoot;

    Task<int> SaveChangesAsync();
}