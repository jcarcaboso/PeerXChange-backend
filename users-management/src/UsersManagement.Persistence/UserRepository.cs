using Mapster;
using ErrorOr;
using MassTransit;
using Microsoft.Extensions.Options;
using UsersManagement.Domain;
using UsersManagement.Domain.Exceptions;
using UsersManagement.Domain.Factories;
using UsersManagement.Domain.Repositories;
using UsersManagement.Persistence.Models;
using UsersManagement.Persistence.Events;
using Role = UsersManagement.Domain.Role;
using User = UsersManagement.Persistence.Models.User;

namespace UsersManagement.Persistence;

internal sealed class UserRepository(
    IOptionsMonitor<UserSettings> options,
    UsersManagementContext context,
    IPublishEndpoint publishEndpoint,
    IUserFactory userFactory) : IUserRepository
{
    public async Task<ErrorOr<bool>> TryCreateUserAsync(Domain.User user, CancellationToken cancellationToken = default)
    {
        var wallet = await context.Users.FindAsync([user.Wallet.ToString()], cancellationToken: cancellationToken);

        if (wallet is not null)
        {
            return Error.Conflict($"User {wallet.Wallet} already exist");
        }

        var newUser = user.Adapt<User>();

        await context.Users.AddAsync(newUser, cancellationToken);
        await publishEndpoint.Publish(new UserCreatedEvent(user.Wallet), cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<ErrorOr<Domain.User>> TryGetUserAsync(
        Domain.Address userId,
        CancellationToken cancellationToken = default)
    {
        var wallet = await context.Users.FindAsync([userId.ToString()], cancellationToken: cancellationToken);

        if (wallet is null or { IsActive: false } or { IsDeleted: true })
        {
            return Error.NotFound($"User {userId} not found");
        }

        var role = (Role)wallet.Role;

        var userWithoutRole = wallet.Adapt<Domain.UserWithoutRole>();
        var user = role switch
        {
            Role.Admin => userFactory.CreateAdminUser(userWithoutRole),
            Role.User => userFactory.CreateUser(userWithoutRole),
            _ => null,
        };

        return user is null
            ? Error.Failure($"Invalid rol for user {userId}")
            : user!;
    }

    public async Task<ErrorOr<bool>> UpdateUserAsync(Domain.PartialUser user,
        CancellationToken cancellationToken = default)
    {
        var wallet = await context.Users.FindAsync([user.Wallet.ToString()], cancellationToken: cancellationToken);

        if (wallet is null)
        {
            return Error.NotFound($"User {user.Wallet} not found");
        }

        wallet.Language = user.Language is null ? wallet.Language : user.Language.Value.ToString().ToLowerInvariant();

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<ErrorOr<bool>> DeleteUserAsync(Domain.Address userId,
        CancellationToken cancellationToken = default)
    {
        var wallet = await context.Users.FindAsync([userId.ToString()], cancellationToken: cancellationToken);

        if (wallet is null)
        {
            return Error.NotFound($"User {userId} not found");
        }

        wallet.IsDeleted = true;
        wallet.DeleteDeadline = DateTime.UtcNow.AddDays(options.CurrentValue.Delete.GracePeriodInDays);

        await publishEndpoint.Publish(
            new UserMarkedAsDeletedEvent(wallet.Wallet, wallet.DeleteDeadline.Value),
            cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<ErrorOr<bool>> UndoDeleteUserAsync(Address userId, CancellationToken cancellationToken = default)
    {
        var wallet = await context.Users.FindAsync([userId.ToString()], cancellationToken: cancellationToken);

        if (wallet is null or { IsDeleted: false })
        {
            return Error.NotFound($"User {userId} not found");
        }

        wallet.IsDeleted = false;
        wallet.DeleteDeadline = null;

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}