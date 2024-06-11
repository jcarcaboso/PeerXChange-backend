using PeerXChange.Common;

namespace UsersManagement.Domain;

public sealed record UserWithoutRole
{
    public required Address Wallet { init; get; }
    public required Language Language { init; get; }
}