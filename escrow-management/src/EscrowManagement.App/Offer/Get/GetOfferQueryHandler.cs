using MediatR;

namespace EscrowManagement.App.Offer.Get;

public struct GetOfferQueryHandler : IRequestHandler<GetOfferQuery, GetOfferQueryResponse>
{
    public Task<GetOfferQueryResponse> Handle(GetOfferQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
