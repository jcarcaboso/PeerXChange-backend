using PeerXChange.Common;

namespace UsersManagement.Domain;

public sealed record PartialUser : UserBase
{
    public Language? Language { set; get; }
}