using MediatR;

namespace EscrowManagement.App.Offer.Delete;

public struct DeleteOfferCommandHandler : IRequestHandler<DeleteOfferCommand, DeleteOfferCommandResponse>
{
    public Task<DeleteOfferCommandResponse> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
