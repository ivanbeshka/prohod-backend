namespace Prohod.Domain.ErrorsBase;

public interface IOperationError
{
    T Accept<T>(IOperationErrorVisitor<T> visitor);
}