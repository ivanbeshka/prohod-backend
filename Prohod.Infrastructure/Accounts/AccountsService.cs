using System.Security.Cryptography;
using Kontur.Results;
using Prohod.Domain.GenericRepository;
using Prohod.Domain.Users;
using Prohod.Infrastructure.Accounts.JwtTokens;
using Prohod.Infrastructure.Accounts.Models;
using Prohod.Infrastructure.Accounts.Models.CreateAccount;
using Prohod.Infrastructure.Accounts.Passwords;
using Prohod.Infrastructure.Accounts.Repository;

namespace Prohod.Infrastructure.Accounts;

public class AccountsService : IAccountsService
{
    private readonly IAccountsRepository accountsRepository;
    private readonly IPasswordsHashCalculator passwordsHashCalculator;
    private readonly IJwtTokensGenerator jwtTokensGenerator;

    public AccountsService(
        IAccountsRepository accountsRepository,
        IPasswordsHashCalculator passwordsHashCalculator,
        IJwtTokensGenerator jwtTokensGenerator)
    {
        this.accountsRepository = accountsRepository;
        this.passwordsHashCalculator = passwordsHashCalculator;
        this.jwtTokensGenerator = jwtTokensGenerator;
    }

    public async Task<Result<EntityNotFoundError<Account>, AuthenticatedUser>> AuthenticateAsync(string login, string password)
    {
        var passwordHash = passwordsHashCalculator.CalculatePasswordHash(password); 
        var getAccountResult = await accountsRepository.GetUserByLoginAndPassword(login, passwordHash);

        if (!getAccountResult.TryGetValue(out var user, out var fault))
        {
            return fault;
        }

        var jwtToken = jwtTokensGenerator.GenerateJwtToken(user);
        
        return new AuthenticatedUser(user, jwtToken);
    }

    public async Task<Result<LoginAlreadyExistsError, AccountCredentials>> CreateUserAccountAsync(
        CreateAccountDto requestAccountInfo, Role userRole)
    {
        var ((name, surname, email), login) = requestAccountInfo;
        var isLoginExists = await accountsRepository.IsLoginExists(login);
        if (isLoginExists)
        {
            return new LoginAlreadyExistsError(login);
        }

        var password = GeneratePassword();
        var passwordHash = passwordsHashCalculator.CalculatePasswordHash(password);
        var account = new Account(new(name, surname, email, userRole), login, passwordHash);
        await accountsRepository.AddAsync(account);
        return new AccountCredentials(login, password);
    }

    private static string GeneratePassword()
    {
        const string availableChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890!@#$%&";

        return string.Join("",
            Enumerable
                .Range(0, 12)
                .Select(_ => availableChars[RandomNumberGenerator.GetInt32(availableChars.Length)]));
    }
}