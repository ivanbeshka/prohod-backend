using Prohod.Domain.Users;
using Prohod.Infrastructure.Accounts.JwtTokens;

namespace Prohod.WebApi.Accounts.Models.Login;

public record LoginResponse(User User, JwtToken JwtToken);