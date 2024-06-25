namespace EscrowManagement.Api;

public static class OfferEndpoints
{
    public static void AddOfferEndpoints(this WebApplication webApp)
    {
        webApp.MapGroup("offer")
            .MapCreateOffer()
            .MapGetOffer();
    }

    private static RouteGroupBuilder MapCreateOffer(this RouteGroupBuilder builder)
    {
        builder
            .MapPut("", () => { return Results.Created(); })
            .WithOpenApi();

        return builder;
    }

    private static RouteGroupBuilder MapGetOffer(this RouteGroupBuilder builder)
    {
        builder
            .MapGet("", () => { return Results.Ok(); })
            .WithOpenApi();
        
        return builder;
    }
}