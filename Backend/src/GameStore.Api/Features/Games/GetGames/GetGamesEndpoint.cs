using GameStore.Api.Data;
using GameStore.Api.Models;
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

                IQueryable<Game> filteredGames = dbContext.Games
                                    .Where(game => string.IsNullOrWhiteSpace(request.Name)
                                            || EF.Functions.Like(game.Name, $"%{request.Name}%"));

                List<GameSummaryDto> gamesOnPage = await filteredGames
                                                        .OrderBy(game => game.Name)
                                                        .Skip(skipCount)
                                                        .Take(request.PageSize)
                                                        .Include(game => game.Genre)
                                                        .Select(game => new GameSummaryDto(
                                                            game.Id,
                                                            game.Name,
                                                            game.Genre!.Name,
                                                            game.Price,
                                                            game.ReleaseDate,
                                                            game.ImageUri
                                                        ))
                                                        .AsNoTracking()
                                                        .ToListAsync();

                int totalGames = await filteredGames.CountAsync();
                int totalPages = (int)Math.Ceiling(totalGames / (double)request.PageSize);

                return new GamesPageDto(totalPages, gamesOnPage);
            });
    }
}
