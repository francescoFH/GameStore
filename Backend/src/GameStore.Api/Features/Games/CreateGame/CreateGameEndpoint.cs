using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;
using GameStore.Api.FileUpload;
using GameStore.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
    private const string DefaulImageUri = "https://placehold.co/100";
    public static void MapCreateGame(
        this IEndpointRouteBuilder app)
    {
        // POST /games
        app.MapPost("/", async (
            [FromForm] CreateGameDto gameDto,
            GameStoreContext dbContext,
            ILogger<Program> logger,
            FileUploader fileUploader) =>
        {
            string imageUri = DefaulImageUri;

            if (gameDto.ImageFile is not null)
            {
                var fileUploadResults = await fileUploader.UploadFileAsync(
                    gameDto.ImageFile,
                    StorageNames.GameImagesFolder
                );

                if (!fileUploadResults.IsSuccess)
                {
                    return Results.BadRequest(new { message = fileUploadResults.ErrorMessage });
                }

                imageUri = fileUploadResults.FIleUrl!;
            }

            Game game = new()
            {
                Name = gameDto.Name,
                GenreId = gameDto.GenreId,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                Description = gameDto.Description,
                ImageUri = imageUri!
            };

            dbContext.Games.Add(game);

            await dbContext.SaveChangesAsync();

            logger.LogInformation(
                "Created game {GameName} with price {GamePrice}",
                game.Name,
                game.Price);

            return Results.CreatedAtRoute(
                EndpointNames.GetGame,
                new { id = game.Id },
                new GameDetailsDto(
                    game.Id,
                    game.Name,
                    game.GenreId,
                    game.Price,
                    game.ReleaseDate,
                    game.Description,
                    game.ImageUri
                ));
        })
        .WithParameterValidation()
        .DisableAntiforgery();
    }
}
