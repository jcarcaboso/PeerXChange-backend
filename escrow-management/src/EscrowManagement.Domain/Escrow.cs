using PeerXChange.Common;

namespace EscrowManagement.Domain;

public record Escrow
{
    public required string SellerId { get; init; }
    public required string BuyerId { get; init; }
    
    public required string OfferId { get; init; }
    
    public required Guid Id { get; init; }
    
    public required EscrowStatus Status { get; init; }
    
    public required decimal Amount { get; init; }
    public required Currency Currency { get; init; }
    
    public required long Conversion { get; init; }
    
    public required string ChainId { get; init; }
    public required string TokenId { get; init; }
}