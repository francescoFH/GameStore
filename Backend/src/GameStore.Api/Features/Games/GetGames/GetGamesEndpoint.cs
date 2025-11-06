using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Games.GetGames;

public static class GetGamesEndpoint
{
    public static void MapGetGames(
        this IEndpointRouteBuilder app)
    {
        // GET /games
        app.MapGet("/", async (
            GameStoreContext dbContext,
            [AsParameters] GetGamesDto request) =>
            {
                int skipCount = (request.PageNumber - 1) * request.PageSize;

                List<GameSummaryDto> gamesOnPage = await dbContext.Games
                                                            .OrderBy(game => game.Name)
                                                            .Skip(skipCount)
                                                            .Take(request.PageSize)
                                                            .Include(game => game.Genre)
                                                            .Select(game => new GameSummaryDto(
                                                                game.Id,
                                                                game.Name,
                                                                game.Genre!.Name,
                                                                game.Price,
                                                                game.ReleaseDate
                                                            ))
                                                            .AsNoTracking()
                                                            .ToListAsync();

                int totalGames = await dbContext.Games.CountAsync();
                int totalPages = (int)Math.Ceiling(totalGames / (double)request.PageSize);

                return new GamesPageDto(totalPages, gamesOnPage);
            });
    }
}
