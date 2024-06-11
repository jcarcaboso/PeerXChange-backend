using PeerXChange.Common;

namespace UsersManagement.Api.Contracts;

public sealed record UpdateUserRequest
{
    public Language Language { get; set; }
}