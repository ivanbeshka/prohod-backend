using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Prohod.Domain.Users;

namespace Prohod.WebApi.Authorization;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class AuthorizedRolesAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly Role[] roles;

    public AuthorizedRolesAttribute(params Role[]? roles)
    {
        this.roles = roles ?? Array.Empty<Role>();
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var isUserInRole = roles.Any(role => context.HttpContext.User.IsInRole(role.ToString())) || roles.Length == 0;

        if (!isUserInRole)
        {
            context.Result = new JsonResult(new { message = "User with presented token doesn't have access to method" })
                { StatusCode = StatusCodes.Status403Forbidden };
        }
    }
}