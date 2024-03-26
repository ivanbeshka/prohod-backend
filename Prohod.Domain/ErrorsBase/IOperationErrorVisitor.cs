using Prohod.Domain.AggregationRoot;
using Prohod.Domain.GenericRepository;
using Prohod.Domain.VisitRequests;

namespace Prohod.Domain.ErrorsBase;

public interface IOperationErrorVisitor<out T>
{
    T Visit<TEntity>(EntityNotFoundError<TEntity> error) 
        where TEntity : IAggregationRoot;

    T Visit(ProcessVisitRequestError error);
}