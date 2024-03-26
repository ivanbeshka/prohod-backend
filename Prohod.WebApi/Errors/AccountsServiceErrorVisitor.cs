using Microsoft.AspNetCore.Mvc;
using Prohod.Infrastructure.Accounts.Errors;
using Prohod.Infrastructure.Accounts.Models.CreateAccount;

namespace Prohod.WebApi.Errors;

public class AccountsServiceErrorVisitor : IAccountsServiceErrorVisitor<ActionResult>
{
    public ActionResult Visit(LoginAlreadyExistsError error)
    {
        return ToError(StatusCodes.Status409Conflict, $"Account with login = {error.Login} already exists");
    }
    
    private static ActionResult ToError(int statusCode, string description)
    {
        return new ObjectResult(description) { StatusCode = statusCode };
    }
}