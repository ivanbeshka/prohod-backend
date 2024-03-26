using Prohod.Domain.Users;

namespace Prohod.Infrastructure.Accounts.JwtTokens;

public interface IJwtTokensGenerator
{
    JwtToken GenerateJwtToken(User user);
}