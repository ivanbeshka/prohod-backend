using Kontur.Results;
using Prohod.Domain.AggregationRoot;
using Prohod.Domain.GenericRepository;

namespace Prohod.Infrastructure.Database;

public abstract class RepositoryBase<T> : IRepository<T> 
    where T : class, IAggregationRoot
{
    private readonly IAppDbContext dbContext;

    protected RepositoryBase(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddAsync(T entity)
    {
        await dbContext.Set<T>().AddAsync(entity);

        await dbContext.SaveChangesAsync();
    }

    public async Task<Result<EntityNotFoundError<T>, T>> FindAsync(Guid id)
    {
        var foundEntity = await dbContext.Set<T>().FindAsync(id);

        return foundEntity is null 
            ? EntityNotFoundError<T>.FromId(id)
            : foundEntity;
    }

    public async Task UpdateAsync(T entity)
    {
        dbContext.Set<T>().Update(entity);

        await dbContext.SaveChangesAsync();
    }
}