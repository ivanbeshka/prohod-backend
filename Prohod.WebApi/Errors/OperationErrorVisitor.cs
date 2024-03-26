using Microsoft.AspNetCore.Mvc;
using Prohod.Domain.AggregationRoot;
using Prohod.Domain.ErrorsBase;
using Prohod.Domain.GenericRepository;
using Prohod.Domain.VisitRequests;

namespace Prohod.WebApi.Errors;

public class OperationErrorVisitor : IOperationErrorVisitor<ActionResult>
{
    public ActionResult Visit<T>(EntityNotFoundError<T> error)
        where T : IAggregationRoot
    {
        return ToError(StatusCodes.Status404NotFound, error.Message);
    }

    public ActionResult Visit(ProcessVisitRequestError error)
    {
        return ToError(StatusCodes.Status400BadRequest, error.Message);
    }

    private static ActionResult ToError(int statusCode, string description)
    {
        return new ObjectResult(description) { StatusCode = statusCode };
    }
}