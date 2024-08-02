using MediatR;

namespace EscrowManagement.App.Offer.Create;

public struct CreateOfferCommandHandler : IRequestHandler<CreateOfferCommand, CreateOfferCommandResponse>
{
    public Task<CreateOfferCommandResponse> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
