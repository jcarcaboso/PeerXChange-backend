using MediatR;

namespace EscrowManagement.App.Offer.Update;

public struct UpdateOfferCommandHandler : IRequestHandler<UpdateOfferCommand, UpdateOfferCommandResponse>
{
    public Task<UpdateOfferCommandResponse> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
