using Prohod.Domain.ErrorsBase;

namespace Prohod.Domain.VisitRequests;

public record ProcessVisitRequestError(string Message) : IOperationError
{
    public T Accept<T>(IOperationErrorVisitor<T> visitor) => visitor.Visit(this);
}