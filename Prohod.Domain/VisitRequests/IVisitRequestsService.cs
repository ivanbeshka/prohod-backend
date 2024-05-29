using Kontur.Results;
using Prohod.Domain.ErrorsBase;
using Prohod.Domain.Forms;
using Prohod.Domain.GenericRepository;
using Prohod.Domain.Users;

namespace Prohod.Domain.VisitRequests;

public interface IVisitRequestsService
{
    Task<Result<EntityNotFoundError<User>>> ApplyFormAsync(ApplyFormDto form);

    Task<Result<IOperationError>> AcceptRequestAsync(Guid visitRequestId, Guid whoAcceptedId);
    
    Task<Result<IOperationError>> RejectRequestAsync(Guid visitRequestId, Guid whoProcessedId, string rejectionReason);

    Task<IReadOnlyList<VisitRequest>> GetActiveVisitRequestsPage(int offset, int limit);

    Task<IReadOnlyList<VisitRequest>> GetUnactiveVisitRequestsPage(int offset, int limit);

    Task<IReadOnlyList<VisitRequest>> GetVisitRequestsPage(int offset, int limit);
    
    Task<Result<EntityNotFoundError<User>, IReadOnlyList<VisitRequest>>> GetUserProcessedVisitRequestsPage(
        Guid userId, int offset, int limit);
}