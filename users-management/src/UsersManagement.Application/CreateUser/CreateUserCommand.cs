using ErrorOr;
using MediatR;
using PeerXChange.Common;
using UsersManagement.Domain;

namespace UsersManagement.Application.CreateUser;

public sealed record CreateUserCommand : IRequest<ErrorOr<Unit>>
{
    public required Address Wallet { init; get; }
    public required Language Language { init; get; }
}