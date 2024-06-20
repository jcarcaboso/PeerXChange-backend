using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using Mapster;

namespace UsersManagement.Api;

public class ErrorOrBaseController : ControllerBase
{
    protected IActionResult MapErrorOrResponse<T>(ErrorOr<T> response, IActionResult actionResult) =>
        response.Match(_ => actionResult, MapErrorResponse);
    
    protected IActionResult MapErrorOrOkResponse<T>(ErrorOr<T> response) => 
        response.Match(v => Ok(v), MapErrorResponse);
    
    protected IActionResult MapErrorOrOkResponse<T, U>(ErrorOr<T> response) => 
        response.Match(v => Ok(v.Adapt<U>()), MapErrorResponse);
    
    protected IActionResult MapErrorOrResponse<T>(ErrorOr<T> response, Func<T, IActionResult> success) => 
        response.Match(success, MapErrorResponse);

    private IActionResult MapErrorResponse(IEnumerable<Error> errors)
    {
        var error = errors.First();
        
        return error.Type switch
        {
            ErrorType.NotFound => NotFound(MapError(error)),
            // ErrorType.Failure => expr,
            // ErrorType.Unexpected => expr,
            ErrorType.Validation => BadRequest(MapError(error)),
            ErrorType.Conflict => Conflict(MapError(error)),
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
}