using Kontur.Results;
using Prohod.Domain.AggregationRoot;

namespace Prohod.Domain.GenericRepository;

public interface IRepository<T> where T : IAggregationRoot
{
    Task AddAsync(T entity);

    Task<Result<EntityNotFoundError<T>, T>> FindAsync(Guid id);

    Task UpdateAsync(T entity);
}