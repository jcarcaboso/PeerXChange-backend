using MediatR;
using UsersManagement.Domain.Repositories;

namespace UsersManagement.Application.DeleteUser;

public sealed class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var response = await userRepository.DeleteUserAsync(request.UserId, cancellationToken);
        return response;
    }
}