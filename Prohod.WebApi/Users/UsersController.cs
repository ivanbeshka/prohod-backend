using Microsoft.AspNetCore.Mvc;
using Prohod.Domain.Users;
using Prohod.WebApi.Authorization;
using Prohod.WebApi.Users.Models.GetAvailableToVisitUsers;

namespace Prohod.WebApi.Users;

[Route("api/v1/users")]
public class UsersController : ControllerBase
{
    private readonly IUsersRepository usersRepository;

    public UsersController(IUsersRepository usersRepository)
    {
        this.usersRepository = usersRepository;
    }

    [AuthorizedRoles(Role.Security, Role.Admin, Role.User)]
    [HttpGet()]
    public async Task<ActionResult<GetAvailableToVisitUsersResponse>> GetAvailableToVisitUsers()
    {
        return Ok(new GetAvailableToVisitUsersResponse(await usersRepository.GetAvailableToVisitUsersAsync()));
    }
}