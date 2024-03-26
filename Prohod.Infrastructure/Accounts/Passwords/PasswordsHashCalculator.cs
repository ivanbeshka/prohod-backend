using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Prohod.Infrastructure.Accounts.Passwords.Options;

namespace Prohod.Infrastructure.Accounts.Passwords;

public class Sha256PasswordsHashCalculator : IPasswordsHashCalculator
{
    private readonly IOptions<PasswordsSalt> salt;

    public Sha256PasswordsHashCalculator(IOptions<PasswordsSalt> salt)
    {
        this.salt = salt;
    }
    
    public string CalculatePasswordHash(string password)
    {
        var saltedPasswordBytes = Encoding.UTF8.GetBytes(password + salt.Value.Value);
        
        var hash = SHA256.HashData(saltedPasswordBytes);

        var hashString = Convert.ToHexString(hash);

        return hashString;
    }
}