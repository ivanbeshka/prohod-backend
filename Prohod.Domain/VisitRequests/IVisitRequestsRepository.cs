using Prohod.Domain.GenericRepository;

namespace Prohod.Domain.VisitRequests;

public interface IVisitRequestsRepository : IRepository<VisitRequest>
{
    Task<IReadOnlyList<VisitRequest>> GetActiveVisitRequestsPageAsync(int offset, int limit);

    Task<IReadOnlyList<VisitRequest>> GetUnactiveVisitRequestsPageAsync(int offset, int limit);

    Task<IReadOnlyList<VisitRequest>> GetVisitRequestsPageAsync(int offset, int limit);

    Task<IReadOnlyList<VisitRequest>> GetUserProcessedVisitRequestsPageAsync(Guid userId, int offset, int limit);
}