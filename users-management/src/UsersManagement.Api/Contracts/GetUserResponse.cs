using PeerXChange.Common;
using UsersManagement.Domain;

namespace UsersManagement.Api.Contracts;

public sealed record GetUserResponse()
{
    public string Address { set; get; }
    public Role Role { set; get; }
    public Language Language { set; get; }
}