using Microsoft.AspNetCore.Mvc;
using Prohod.Domain.ErrorsBase;
using Prohod.Domain.Users;
using Prohod.Domain.VisitRequests;
using Prohod.WebApi.Authorization;
using Prohod.WebApi.VisitRequests.Models.AcceptVisitRequest;
using Prohod.WebApi.VisitRequests.Models.ApplyForm;
using Prohod.WebApi.VisitRequests.Models.GetVisitRequestsPage;
using Prohod.WebApi.VisitRequests.Models.RejectVisitRequest;

namespace Prohod.WebApi.VisitRequests;

[Route("/api/v1/visit-requests")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class VisitRequestsController : ControllerBase
{
    private readonly IVisitRequestsService visitRequestsService;
    private readonly IOperationErrorVisitor<ActionResult> errorVisitor;

    public VisitRequestsController(
        IVisitRequestsService visitRequestsService,
        IOperationErrorVisitor<ActionResult> errorVisitor)
    {
        this.visitRequestsService = visitRequestsService;
        this.errorVisitor = errorVisitor;
    }

    [HttpPost("apply")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> ApplyForm([FromBody] ApplyFormRequest request)
    {
        var applyResult = await visitRequestsService.ApplyFormAsync(request.Form);

        return applyResult.TryGetFault(out var fault)
            ? fault.Accept(errorVisitor)
            : CreatedAtAction(nameof(GetNotProcessedVisitRequestsPage), null);
    }
    
    [AuthorizedRoles(Role.Security)]
    [HttpGet("statuses/not-processed")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetVisitRequestsPageResponse>> GetNotProcessedVisitRequestsPage(
        int offset = 0, int limit = 10)
    {
        var notProcessedVisitRequests = await visitRequestsService.GetNotProcessedVisitRequestsPage(offset, limit);

        return Ok(new GetVisitRequestsPageResponse(notProcessedVisitRequests));
    }
    
    [AuthorizedRoles(Role.Security)]
    [HttpGet("who-processed/{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetVisitRequestsPageResponse>> GetUserProcessedVisitRequestsPage(
        [FromRoute] Guid userId, int offset = 0, int limit = 10)
    {
        var userProcessedVisitRequestsResult =
            await visitRequestsService.GetUserProcessedVisitRequestsPage(userId, offset, limit);

        return userProcessedVisitRequestsResult.TryGetValue(out var visitRequests, out var fault)
            ? Ok(new GetVisitRequestsPageResponse(visitRequests))
            : fault.Accept(errorVisitor);
    }

    [AuthorizedRoles(Role.Admin)]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetVisitRequestsPageResponse>> GetVisitRequestsPage(int offset = 0, int limit = 10)
    {
        var allVisitRequests = await visitRequestsService.GetVisitRequestsPage(offset, limit);

        return Ok(new GetVisitRequestsPageResponse(allVisitRequests));
    }
    
    [AuthorizedRoles(Role.Security)]
    [HttpPost("{visitRequestId:guid}/accept")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> AcceptVisitRequest([FromRoute] Guid visitRequestId, [FromBody] AcceptVisitRequestRequest request)
    {
        var acceptResult = 
            await visitRequestsService.AcceptRequestAsync(visitRequestId, request.WhoAcceptedId);

        return acceptResult.TryGetFault(out var fault) 
            ? fault.Accept(errorVisitor)
            : NoContent();
    }
    
    [AuthorizedRoles(Role.Security)]
    [HttpPost("{visitRequestId:guid}/reject")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> RejectVisitRequest([FromRoute] Guid visitRequestId, [FromBody] RejectVisitRequestRequest request)
    {
        var rejectResult = 
            await visitRequestsService.RejectRequestAsync(
                visitRequestId, request.WhoRejectedId, request.RejectionReason);

        return rejectResult.TryGetFault(out var fault) 
            ? fault.Accept(errorVisitor)
            : NoContent();
    }
}