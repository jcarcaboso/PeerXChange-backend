using PeerXChange.Common;

namespace UsersManagement.Domain;

public sealed record User : UserBase
{
    internal User()
    {
    }

    public required Language Language { init; get; }
}