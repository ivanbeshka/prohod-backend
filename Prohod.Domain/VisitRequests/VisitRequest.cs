using Kontur.Results;
using Prohod.Domain.AggregationRoot;
using Prohod.Domain.Forms;
using Prohod.Domain.Users;

namespace Prohod.Domain.VisitRequests;

public class VisitRequest : IAggregationRoot
{
    public Guid Id { get; private set; }
    
    public virtual Form Form { get; private set; }

    public virtual User? WhoProcessed { get; private set; }

    public VisitRequestStatus Status { get; private set; }

    public string? RejectionReason { get; private set; }
    
#pragma warning disable CS8618
    protected VisitRequest() { }
#pragma warning restore CS8618

    public VisitRequest(Form form)
    {
        Id = Guid.NewGuid();
        Form = form;
        WhoProcessed = null;
        Status = VisitRequestStatus.NotProcessed;
        RejectionReason = null;
    }

    public Result<ProcessVisitRequestError> AcceptRequest(User whoAccepted)
    {
        return ValidateUser(whoAccepted)
            .OnSuccess(() =>
            {
                WhoProcessed = whoAccepted;
                Status = VisitRequestStatus.Accept;
            });
    }

    public Result<ProcessVisitRequestError> RejectRequest(User whoRejected, string rejectionReason)
    {
        return ValidateUser(whoRejected)
            .OnSuccess(() =>
            {
                WhoProcessed = whoRejected;
                RejectionReason = rejectionReason;
                Status = VisitRequestStatus.Reject;
            });
    }

    private Result<ProcessVisitRequestError> ValidateUser(User whoProcessed)
    {
        if (whoProcessed.Role is not Role.Security)
        {
            return new ProcessVisitRequestError($"Only users with role {Role.Security} can reject visit requests");
        }
        
        if (Status is not VisitRequestStatus.NotProcessed)
        {
            return new ProcessVisitRequestError("Can't process already processed request");
        }

        return Result.Succeed();
    }
}