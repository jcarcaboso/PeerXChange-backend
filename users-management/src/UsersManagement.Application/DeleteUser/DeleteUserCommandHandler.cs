using ErrorOr;
using MediatR;
using UsersManagement.Domain.Repositories;

namespace UsersManagement.Application.DeleteUser;

public sealed class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var result = await userRepository.DeleteUserAsync(request.UserId, cancellationToken);
        
        return result.Match<ErrorOr<Unit>>(
            value => value ? Unit.Value : Error.Unexpected(), 
            err => err);
    }
}