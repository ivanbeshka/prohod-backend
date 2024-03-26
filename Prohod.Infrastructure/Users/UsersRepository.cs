using Microsoft.EntityFrameworkCore;
using Prohod.Domain.Users;
using Prohod.Infrastructure.Database;

namespace Prohod.Infrastructure.Users;

public class UsersRepository : RepositoryBase<User>, IUsersRepository
{
    private readonly IAppDbContext dbContext;

    public UsersRepository(IAppDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<bool> ExistsAsync(Guid userId)
    {
        return await dbContext.Set<User>().AnyAsync(user => user.Id == userId);
    }

    public async Task<IReadOnlyList<User>> GetAvailableToVisitUsersAsync()
    {
        return await dbContext.Set<User>().Where(user => user.Role == Role.User).ToListAsync();
    }
}