using ErrorOr;
using Mapster;
using MediatR;
using UsersManagement.Domain;
using UsersManagement.Domain.Repositories;

namespace UsersManagement.Application.UpdateUser;

public sealed class UpdateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userUpdate = request.Adapt<PartialUser>();

        var result = await userRepository.UpdateUserAsync(userUpdate, cancellationToken);
        
        return result.Match<ErrorOr<Unit>>(
            value => value ? Unit.Value : Error.Unexpected(), 
            err => err);
    }
}