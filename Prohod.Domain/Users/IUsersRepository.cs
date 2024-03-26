using Prohod.Domain.GenericRepository;

namespace Prohod.Domain.Users;

public interface IUsersRepository : IRepository<User>
{
    Task<bool> ExistsAsync(Guid userId);
    
    Task<IReadOnlyList<User>> GetAvailableToVisitUsersAsync();
}