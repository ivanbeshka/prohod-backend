using Prohod.Domain.GenericRepository;

namespace Prohod.Domain.VisitRequests;

public interface IVisitRequestsRepository : IRepository<VisitRequest>
{
    Task<IReadOnlyList<VisitRequest>> GetNotProcessedVisitRequestsPageAsync(int offset, int limit);
    
    Task<IReadOnlyList<VisitRequest>> GetVisitRequestsPageAsync(int offset, int limit);

    Task<IReadOnlyList<VisitRequest>> GetUserProcessedVisitRequestsPageAsync(Guid userId, int offset, int limit);
}