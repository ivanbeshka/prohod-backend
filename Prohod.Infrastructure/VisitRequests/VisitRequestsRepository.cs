using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Prohod.Domain.VisitRequests;
using Prohod.Infrastructure.Database;

namespace Prohod.Infrastructure.VisitRequests;

public class VisitRequestsRepository : RepositoryBase<VisitRequest>, IVisitRequestsRepository
{
    private readonly IAppDbContext dbContext;

    public VisitRequestsRepository(IAppDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public Task<IReadOnlyList<VisitRequest>> GetActiveVisitRequestsPageAsync(int offset, int limit)
    {
        return GetVisitRequestsPageAsync(request => ((DateTimeOffset) request.Form.VisitTime).Date >= ((DateTimeOffset) DateTime.Now).Date, offset, limit);
    }

    public Task<IReadOnlyList<VisitRequest>> GetUnactiveVisitRequestsPageAsync(int offset, int limit)
    {
        return GetVisitRequestsPageAsync(request => ((DateTimeOffset) request.Form.VisitTime).Date < ((DateTimeOffset)DateTime.Now).Date, offset, limit);
    }

    public Task<IReadOnlyList<VisitRequest>> GetVisitRequestsPageAsync(int offset, int limit)
    {
        return GetVisitRequestsPageAsync(request => true, offset, limit);
    }

    public Task<IReadOnlyList<VisitRequest>> GetUserProcessedVisitRequestsPageAsync(
        Guid userId, int offset, int limit)
    {
        return GetVisitRequestsPageAsync(
            request => request.WhoProcessed != null && request.WhoProcessed.Id == userId, offset, limit);
    }
    
    private async Task<IReadOnlyList<VisitRequest>> GetVisitRequestsPageAsync(
        Expression<Func<VisitRequest, bool>> filter, int offset, int limit)
    {
        var requests = dbContext.Set<VisitRequest>();
        
        return await requests
            .Where(filter)
            .Skip(offset)
            .Take(limit)
            .OrderBy(request => request.Form.VisitTime)
            .ToListAsync();
    }
}