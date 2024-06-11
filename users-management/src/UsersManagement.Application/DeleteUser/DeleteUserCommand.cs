using MediatR;
using UsersManagement.Domain;

namespace UsersManagement.Application.DeleteUser;

public sealed record DeleteUserCommand(Address UserId) : IRequest<bool>;