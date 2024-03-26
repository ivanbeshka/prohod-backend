namespace Prohod.WebApi.VisitRequests.Models.RejectVisitRequest;

public record RejectVisitRequestRequest(Guid WhoRejectedId, string RejectionReason);