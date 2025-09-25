using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Games.DeleteGame;

public static class DeleteGameEndpoint
{
    public static void MapDeleteGame(
        this IEndpointRouteBuilder app)
    {
        // DELETE /games/122233-434d-43434....
        app.MapDelete("/{id}", (Guid id, GameStoreContext dbContext) =>
        {
            dbContext.Games
                     .Where(game => game.Id == id)
                     .ExecuteDelete();

            return Results.NoContent();
        });
    }
}
