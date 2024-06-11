namespace UsersManagement.Domain.Factories;

public interface IUserFactory
{
    public User CreateUser(UserWithoutRole userWithoutRole);
    public User CreateAdminUser(UserWithoutRole userWithoutRole);
}