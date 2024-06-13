using Mapster;
using MediatR;
using ErrorOr;
using UsersManagement.Infrastructure.Repositories;

namespace UsersManagement.Application.GetUser;

public sealed class GetUserQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserQuery, ErrorOr<GetUserQueryResponse>>
{
    public async Task<ErrorOr<GetUserQueryResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var result = await userRepository.TryGetUserAsync(request.Wallet, cancellationToken);
        
        return result.Match<ErrorOr<GetUserQueryResponse>>(
            user => user.Adapt<GetUserQueryResponse>(), 
            err => err);
    }
}