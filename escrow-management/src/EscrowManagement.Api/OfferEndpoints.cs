namespace EscrowManagement.Api;

public static class OfferEndpoints
{
    public static void AddOfferEndpoints(this WebApplication webApp)
    {
        webApp.MapGroup("offer")
            .MapCreateOffer();
    }

    private static RouteHandlerBuilder MapCreateOffer(this RouteGroupBuilder builder)
    {
        return builder.MapPut("", () => {});
    }
}