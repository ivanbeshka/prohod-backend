using Kontur.Results;
using Prohod.Domain.GenericRepository;
using Prohod.Domain.Users;
using Prohod.Infrastructure.Accounts.Models;
using Prohod.Infrastructure.Accounts.Models.CreateAccount;

namespace Prohod.Infrastructure.Accounts;

public interface IAccountsService
{
    Task<Result<EntityNotFoundError<Account>, AuthenticatedUser>> AuthenticateAsync(
        string login, string password);

    Task<Result<LoginAlreadyExistsError, AccountCredentials>> CreateUserAccountAsync(
        CreateAccountDto requestAccountInfo, Role userRole);
}