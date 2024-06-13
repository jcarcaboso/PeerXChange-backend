using System.Net;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PeerXChange.Common;
using UsersManagement.Api.Contracts;
using UsersManagement.Application.CreateUser;
using UsersManagement.Application.DeleteUser;
using UsersManagement.Application.GetUser;
using UsersManagement.Application.UpdateUser;

namespace UsersManagement.Api;

[ApiController, Route("api/[controller]")]
public class UserController(IMediator mediator) : ErrorOrBaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(GetUserQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUser(
        [FromQuery] string wallet,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetUserQuery(wallet), cancellationToken);

        return MapErrorOrOkResponse(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateUser(
        [FromQuery] string wallet,
        [FromQuery] Language language,
        CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand() { Wallet = wallet, Language = language };

        var response = await mediator.Send(command, cancellationToken);

        return MapErrorOrResponse(response, Created());
    }

    [HttpPatch("{userId:required}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateUser(
        string userId,
        [FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = request.Adapt<UpdateUserCommand>() with { Wallet = userId };
        
        var response = await mediator.Send(command, cancellationToken);
        
        return MapErrorOrResponse(response, NoContent());
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUser([FromQuery] string userId, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new DeleteUserCommand(userId), cancellationToken);
        
        return MapErrorOrResponse(response, NoContent());
    }
}