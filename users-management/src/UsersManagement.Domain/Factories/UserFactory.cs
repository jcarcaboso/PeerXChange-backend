using PeerXChange.Common;

namespace UsersManagement.Domain.Factories;

public sealed class UserFactory : IUserFactory
{
    public User CreateUser(UserWithoutRole userWithoutRole) => 
        CreateUserWithRole(userWithoutRole, isAdmin: false);

    public User CreateAdminUser(UserWithoutRole userWithoutRole) => 
        CreateUserWithRole(userWithoutRole, isAdmin: true);

    private static User CreateUserWithRole(UserWithoutRole userWithoutRole, bool isAdmin = false)
    {
        return new User()
        {
            Role = isAdmin ? Role.Admin : Role.User,
            Wallet = userWithoutRole.Wallet,
            Language = userWithoutRole.Language
        };
    }
}