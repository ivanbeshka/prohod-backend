using Prohod.Infrastructure.Accounts.Errors;

namespace Prohod.Infrastructure.Accounts.Models.CreateAccount;

public record LoginAlreadyExistsError(string Login) : IAccountsServiceError
{
    public T Accept<T>(IAccountsServiceErrorVisitor<T> visitor) => visitor.Visit(this);
}