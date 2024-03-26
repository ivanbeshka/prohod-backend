using Prohod.Domain.Users;

namespace Prohod.WebApi.Users.Models.GetAvailableToVisitUsers;

public record GetAvailableToVisitUsersResponse(IReadOnlyList<User> Users);