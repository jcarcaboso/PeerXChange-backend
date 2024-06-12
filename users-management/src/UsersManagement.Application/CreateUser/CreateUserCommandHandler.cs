using ErrorOr;
using Mapster;
using MediatR;
using UsersManagement.Domain;
using UsersManagement.Domain.Factories;
using UsersManagement.Domain.Repositories;

namespace UsersManagement.Application.CreateUser;

internal sealed class CreateUserCommandHandler(
    IUserRepository userRepository, 
    IUserFactory userFactory)
    : IRequestHandler<CreateUserCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userWithoutRole = request.Adapt<UserWithoutRole>();

        var user = userFactory.CreateUser(userWithoutRole);
        
        var result = await userRepository.TryCreateUserAsync(user, cancellationToken);

        return result.Match<ErrorOr<Unit>>(
            value => value ? Unit.Value : Error.Unexpected(), 
            err => err);
    }
}