using MediatR;
using UsersManagement.Domain;

namespace UsersManagement.Application.GetUser;

public sealed record GetUserQuery(Address Wallet) : IRequest<GetUserQueryResponse>;


public sealed record GetUserQueryResponse
{
    public required string Address { init; get; }
    public required string Role { get; init; }
    public required string Language { init; get; }
}