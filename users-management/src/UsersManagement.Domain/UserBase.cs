namespace UsersManagement.Domain;

public record UserBase
{
    public required Address Wallet { init; get; }
    public Role Role { init; get; } = Role.User;
}