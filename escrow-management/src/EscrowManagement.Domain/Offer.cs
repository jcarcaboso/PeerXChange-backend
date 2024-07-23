using PeerXChange.Common;

namespace EscrowManagement.Domain;

public record Offer
{
    public required Guid Id { get; init; }
    public required OfferType Type { get; init; }
    
    public required string OwnerId { get; init; }
    
    public required decimal Amount { get; init; }
    public required decimal MinAmount { get; init; }
    public required decimal MaxAmount { get; init; }
    public required Currency Currency { get; init; }
    
    public required long Conversion { get; init; }
    public required IDictionary<string, string> TokenByChain { get; init; }
    
    public required string PaymentDetails { get; init; }
    public string? Description { get; init; }
}
