namespace UsersManagement.Domain.Repositories;

public interface IUserRepository
{
    Task<bool> TryCreateUserAsync(User user, CancellationToken cancellationToken = default);
    Task<(bool, User?)> TryGetUserAsync(Address userId, CancellationToken cancellationToken = default);
    Task<bool> UpdateUserAsync(PartialUser user, CancellationToken cancellationToken = default);
    Task<bool> DeleteUserAsync(Address userId, CancellationToken cancellationToken = default);
    
    // TODO: Manage user deletions undo
    // Task<bool> UndoDeleteUserAsync(Address userId, CancellationToken cancellationToken = default);
}