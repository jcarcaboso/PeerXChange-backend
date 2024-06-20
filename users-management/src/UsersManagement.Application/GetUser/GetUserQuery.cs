using ErrorOr;
using MediatR;
using PeerXChange.Common;
using UsersManagement.Domain;

namespace UsersManagement.Application.GetUser;

public sealed record GetUserQuery(Address Wallet) : IRequest<ErrorOr<GetUserQueryResponse>>;


public sealed record GetUserQueryResponse
{
    public required string Address { init; get; }
    public required Role Role { init; get; }
    public required Language Language { init; get; }
}