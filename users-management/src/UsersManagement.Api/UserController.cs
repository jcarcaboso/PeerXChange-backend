using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PeerXChange.Common;
using UsersManagement.Api.Contracts;
using UsersManagement.Application.CreateUser;
using UsersManagement.Application.DeleteUser;
using UsersManagement.Application.GetUser;
using UsersManagement.Application.UpdateUser;
using UsersManagement.Domain;

namespace UsersManagement.Api;

[ApiController, Route("api/[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUser(
        [FromQuery] Address wallet,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetUserQuery(wallet), cancellationToken);
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(
        [FromQuery] string wallet,
        [FromQuery] Language language,
        CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand() { Wallet = wallet, Language = language };

        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpPatch("{userId:required}")]
    public async Task<IActionResult> UpdateUser(
        string userId,
        [FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = request.Adapt<UpdateUserCommand>() with { Wallet = userId };
        
        await mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser([FromQuery] string userId, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new DeleteUserCommand(userId), cancellationToken);
        
        return NoContent();
    }
}