using Mapster;
using MediatR;
using UsersManagement.Domain;
using UsersManagement.Domain.Repositories;

namespace UsersManagement.Application.UpdateUser;

public sealed class UpdateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userUpdate = request.Adapt<PartialUser>();

        var result = await userRepository.UpdateUserAsync(userUpdate, cancellationToken);

        if (!result)
        {
            // TODO: Return not found
        }
        
        return Unit.Value;
    }
}