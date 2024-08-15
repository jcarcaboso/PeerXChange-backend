namespace EscrowManagement.Domain;

public sealed record User
{
    public required Guid Id { get; init; }
    public required string ExternalId { get; init; }
}