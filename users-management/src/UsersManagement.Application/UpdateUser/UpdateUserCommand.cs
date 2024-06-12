using ErrorOr;
using MediatR;
using PeerXChange.Common;
using UsersManagement.Domain;

namespace UsersManagement.Application.UpdateUser;

public record UpdateUserCommand(Address Wallet, Language? Language) : IRequest<ErrorOr<Unit>>;