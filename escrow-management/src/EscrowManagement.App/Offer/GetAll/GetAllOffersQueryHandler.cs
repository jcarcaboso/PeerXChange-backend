using MediatR;

namespace EscrowManagement.App.Offer.GetAll;

public struct GetAllOffersQueryHandler : IRequestHandler<GetAllOffersQuery, GetAllOffersQueryResponse>
{
    public Task<GetAllOffersQueryResponse> Handle(GetAllOffersQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
