using Prohod.Domain.VisitRequests;

namespace Prohod.WebApi.VisitRequests.Models.GetVisitRequestsPage;

public record GetVisitRequestsPageResponse(IReadOnlyList<VisitRequest> VisitRequests);