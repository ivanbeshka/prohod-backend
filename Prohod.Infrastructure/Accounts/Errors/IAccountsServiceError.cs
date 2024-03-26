namespace Prohod.Infrastructure.Accounts.Errors;

public interface IAccountsServiceError
{
    T Accept<T>(IAccountsServiceErrorVisitor<T> visitor);
}