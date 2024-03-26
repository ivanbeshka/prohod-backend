using Prohod.Domain.AggregationRoot;
using Prohod.Domain.ErrorsBase;

namespace Prohod.Domain.GenericRepository;

public record EntityNotFoundError<TEntity>(string Message) : IOperationError
    where TEntity : IAggregationRoot
{
    public T Accept<T>(IOperationErrorVisitor<T> visitor) => visitor.Visit(this);

    public static EntityNotFoundError<TEntity> FromId(Guid id) 
        => new($"{typeof(TEntity).Name} with id = {id} was not found");
}
