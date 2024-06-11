using Mapster;
using MassTransit;
using Microsoft.Extensions.Options;
using UsersManagement.Domain.Repositories;
using UsersManagement.Persistence.Models;
using UsersManagement.Persistence.Events;

namespace UsersManagement.Persistence;

internal sealed class UserRepository(IOptionsMonitor<UserSettings> options, UsersManagementContext context, IPublishEndpoint publishEndpoint) : IUserRepository
{
    public async Task<bool> TryCreateUserAsync(Domain.User user, CancellationToken cancellationToken = default)
    {
        var wallet = await context.Users.FindAsync([user.Wallet.ToString()], cancellationToken: cancellationToken);

        if (wallet is not null)
        {
            return false;
        }

        var newUser = user.Adapt<User>();

        await context.Users.AddAsync(newUser, cancellationToken);
        await publishEndpoint.Publish(new UserCreatedEvent(user.Wallet), cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<(bool, Domain.User?)> TryGetUserAsync(Domain.Address userId, CancellationToken cancellationToken = default)
    {
        var wallet = await context.Users.FindAsync([userId.ToString()], cancellationToken: cancellationToken);

        return wallet is null
            ? (false, default)
            : (true, wallet.Adapt<Domain.User>());
    }

    public async Task<bool> UpdateUserAsync(Domain.PartialUser user, CancellationToken cancellationToken = default)
    {
        var wallet = await context.Users.FindAsync([user.Wallet.ToString()], cancellationToken: cancellationToken);

        if (wallet is null)
        {
            return false;
        }

        wallet.Language = user.Language is null ? wallet.Language : user.Language.Value.ToString().ToLowerInvariant();

        await context.SaveChangesAsync(cancellationToken);
        
        return true;
    }

    public async Task<bool> DeleteUserAsync(Domain.Address userId, CancellationToken cancellationToken = default)
    {
        var wallet = await context.Users.FindAsync([userId.ToString()], cancellationToken: cancellationToken);

        if (wallet is null)
        {
            return false;
        }

        wallet.IsDeleted = true;
        wallet.DeleteDeadline = DateTime.UtcNow.AddDays(options.CurrentValue.Delete.GracePeriodInDays);
        
        await publishEndpoint.Publish(new UserMarkedAsDeletedEvent(wallet.Wallet, wallet.DeleteDeadline.Value), cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}