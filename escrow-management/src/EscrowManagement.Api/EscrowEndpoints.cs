using Microsoft.AspNetCore.Mvc;

namespace EscrowManagement.Api;

public static class EscrowEndpoints
{
    public static void AddOfferEndpoints(this WebApplication webApp)
    {
        webApp.MapGroup("escrow")
            .MapTakeOffer()
            .MapGetEscrow();
    }

    private static RouteGroupBuilder MapTakeOffer(this RouteGroupBuilder builder)
    {
        builder
            .MapPut("/{userId}/take/{offerId}",
                ([FromRoute]string userId, [FromRoute]string offerId, [FromBody]object body) =>
                {
                    return Results.Created();
                })
            .WithOpenApi();

        return builder;
    }

    private static RouteGroupBuilder MapGetEscrow(this RouteGroupBuilder builder)
    {
        builder
            .MapGet("/{userId}/{escrowId}", (
                [FromRoute] string userId, 
                [FromRoute] string escrowId) =>
            {
                return Results.Ok();
            })
            .WithOpenApi();
        
        return builder;
    }
    
    private static RouteGroupBuilder MapGetEscrows(this RouteGroupBuilder builder)
    {
        builder
            .MapGet("/{userId}/all", (
                [FromRoute] string userId) =>
            {
                return Results.Ok();
            })
            .WithOpenApi();
        
        return builder;
    }
}