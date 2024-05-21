using Microsoft.AspNetCore.Mvc;
using Prohod.Domain.ErrorsBase;
using Prohod.Domain.Users;
using Prohod.Infrastructure.Accounts;
using Prohod.Infrastructure.Accounts.Errors;
using Prohod.WebApi.Accounts.Models.CreateAccount;
using Prohod.WebApi.Accounts.Models.Login;
using Prohod.WebApi.Authorization;

namespace Prohod.WebApi.Accounts;

[Route("/api/v1/accounts")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class AccountsController : ControllerBase
{
    private readonly IAccountsService accountsService;
    private readonly IOperationErrorVisitor<ActionResult> operationErrorVisitor;
    private readonly IAccountsServiceErrorVisitor<ActionResult> accountsServiceErrorVisitor;

    public AccountsController(
        IAccountsService accountsService,
        IOperationErrorVisitor<ActionResult> operationErrorVisitor,
        IAccountsServiceErrorVisitor<ActionResult> accountsServiceErrorVisitor)
    {
        this.accountsService = accountsService;
        this.operationErrorVisitor = operationErrorVisitor;
        this.accountsServiceErrorVisitor = accountsServiceErrorVisitor;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var (login, password) = request;
        var getUserResult = await accountsService.AuthenticateAsync(login, password);

        return getUserResult.TryGetFault(out var fault, out var authenticatedUser)
            ? fault.Accept(operationErrorVisitor)
            : new LoginResponse(authenticatedUser.User, authenticatedUser.Token);
    }

    [HttpPost("create-admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CreateAccountResponse>> CreateAdminAccount(
        [FromBody] CreateAccountRequest request)
    {
        var createAccountResult = await accountsService.CreateUserAccountAsync(request.AccountInfo, Role.Admin);

        return createAccountResult.TryGetFault(out var fault, out var credentials)
            ? fault.Accept(accountsServiceErrorVisitor)
            : new CreateAccountResponse(credentials);
    }

    [AuthorizedRoles(Role.Admin)]
    [HttpPost("security/create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CreateAccountResponse>> CreateSecurityUserAccount(
        [FromBody] CreateAccountRequest request)
    {
        var createAccountResult = await accountsService.CreateUserAccountAsync(request.AccountInfo, Role.Security);

        return createAccountResult.TryGetFault(out var fault, out var credentials)
            ? fault.Accept(accountsServiceErrorVisitor)
            : new CreateAccountResponse(credentials);
    }
    
    [AuthorizedRoles(Role.Security, Role.Admin)]
    [HttpPost("users/create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CreateAccountResponse>> CreateUserAccount(
        [FromBody] CreateAccountRequest request)
    {
        var createAccountResult = await accountsService.CreateUserAccountAsync(request.AccountInfo, Role.User);

        return createAccountResult.TryGetFault(out var fault, out var credentials)
            ? fault.Accept(accountsServiceErrorVisitor)
            : new CreateAccountResponse(credentials);
    }
}