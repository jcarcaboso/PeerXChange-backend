using Mapster;
using MediatR;
using UsersManagement.Domain.Repositories;

namespace UsersManagement.Application.GetUser;

public sealed class GetUserQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserQuery, GetUserQueryResponse>
{
    public async Task<GetUserQueryResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var (exist, user) = await userRepository.TryGetUserAsync(request.Wallet, cancellationToken);

        if (!exist)
        {
            throw new Exception();
        }

        return user.Adapt<GetUserQueryResponse>();
    }
}