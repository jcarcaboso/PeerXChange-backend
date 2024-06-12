using MediatR;
using ErrorOr;
using UsersManagement.Domain;

namespace UsersManagement.Application.DeleteUser;

public sealed record DeleteUserCommand(Address UserId) : IRequest<ErrorOr<Unit>>;