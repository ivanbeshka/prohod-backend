using Prohod.Domain.Users;
using Prohod.Infrastructure.Accounts.JwtTokens;

namespace Prohod.Infrastructure.Accounts.Models;

public record AuthenticatedUser(User User, JwtToken Token);