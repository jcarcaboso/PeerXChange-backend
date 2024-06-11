using Mapster;
using MediatR;
using UsersManagement.Domain;
using UsersManagement.Domain.Factories;
using UsersManagement.Domain.Repositories;

namespace UsersManagement.Application.CreateUser;

internal sealed class CreateUserCommandHandler(IUserRepository userRepository, IUserFactory userFactory)
    : IRequestHandler<CreateUserCommand, Unit>
{
    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userWithoutRole = request.Adapt<UserWithoutRole>();

        var user = userFactory.CreateUser(userWithoutRole);
        
        var isCreated = await userRepository.TryCreateUserAsync(user, cancellationToken);
        
        return Unit.Value;
    }
}