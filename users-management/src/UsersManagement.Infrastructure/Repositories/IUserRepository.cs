using ErrorOr;
using UsersManagement.Domain;

namespace UsersManagement.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<ErrorOr<bool>> TryCreateUserAsync(User user, CancellationToken cancellationToken = default);
    Task<ErrorOr<User>> TryGetUserAsync(Address userId, CancellationToken cancellationToken = default);
    Task<ErrorOr<bool>> UpdateUserAsync(PartialUser user, CancellationToken cancellationToken = default);
    Task<ErrorOr<bool>> DeleteUserAsync(Address userId, CancellationToken cancellationToken = default);
    Task<ErrorOr<bool>> UndoDeleteUserAsync(Address userId, CancellationToken cancellationToken = default);
}