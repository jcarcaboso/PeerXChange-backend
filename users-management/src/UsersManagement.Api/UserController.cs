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
public class UserController(IMediator mediator) : ControllerBase
{
    private IActionResult MapErrorResponse(IEnumerable<Error> errors)
    {
        var error = errors.First();
        
        return error.Type switch
        {
            ErrorType.NotFound => NotFound(MapError(error)),
            // ErrorType.Failure => expr,
            // ErrorType.Unexpected => expr,
            ErrorType.Validation => BadRequest(MapError(error)),
            // ErrorType.Conflict => expr,
            ErrorType.Unauthorized => Unauthorized(MapError(error)),
            // ErrorType.Forbidden => Forbid(MapError(error)),
            _ => throw new Exception(),
        };
    }
    
    private object MapError(Error error)
    {
        return new
        {
            Code = error.Code,
            Description = error.Description,
            Metadata = error.Metadata
        };
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUser(
        [FromQuery] string wallet,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetUserQuery(wallet), cancellationToken);

        return response.IsError ? MapErrorResponse(response.Errors) : Ok(response);
    }

    [HttpPut]
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