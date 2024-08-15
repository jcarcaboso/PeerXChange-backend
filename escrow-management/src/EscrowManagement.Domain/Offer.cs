using PeerXChange.Common;

namespace EscrowManagement.Domain;

public record Offer
{
    public required Guid Id { get; set; }
    public required string OwnerId { get; init; }
    public required OrderType Type { get; init; }
    
    public required OfferPayment Payment { get; init; }
    
    public required PaymentDetails Details { get; init; }
    
    public required string? Comments { get; set; }
}

public sealed record PaymentDetails
{
    
}

public sealed record OfferPayment
{
    public required IDictionary<string, string> TokenByChain { get; init; }
    public required Currency Currency { get; init; }
    public required decimal ConversionRate { get; init; }
    public required decimal TokenAmount { get; init; }
    
    public required decimal MinAmount { get; init; }
    public required decimal MaxAmount { get; init; }
}