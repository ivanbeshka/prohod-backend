namespace Prohod.Infrastructure.Accounts.Passwords;

public interface IPasswordsHashCalculator
{
    string CalculatePasswordHash(string password);
}