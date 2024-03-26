using Prohod.Infrastructure.Accounts.Models.CreateAccount;

namespace Prohod.Infrastructure.Accounts.Errors;

public interface IAccountsServiceErrorVisitor<out T>
{
    T Visit(LoginAlreadyExistsError error);
}