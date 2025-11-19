using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;
using GameStore.Api.FileUpload;
using GameStore.Api.Models;
using GameStore.Api.Shared.FileUpload;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
    public static void MapUpdateGame(
        this IEndpointRouteBuilder app)
    {
        // PUT /games/122233-434d-43434....
        app.MapPut("/{id}", async (
            Guid id,
            [FromForm] UpdateGameDto gameDto,
            GameStoreContext dbContext,
            FileUploader fileUploader,
            ClaimsPrincipal user) =>
        {
            string? currentUserId = user?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrEmpty(currentUserId))
            {
                return Results.Unauthorized();
            }

            Game? existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            if (gameDto.ImageFile is not null)
            {
                FIleUploadResult fileUploadResults = await fileUploader.UploadFileAsync(
                    gameDto.ImageFile,
                    StorageNames.GameImagesFolder
                );

                if (!fileUploadResults.IsSuccess)
                {
                    return Results.BadRequest(new { message = fileUploadResults.ErrorMessage });
                }

                existingGame.ImageUri = fileUploadResults.FIleUrl!;
            }

            existingGame.Name = gameDto.Name;
            existingGame.GenreId = gameDto.GenreId;
            existingGame.Price = gameDto.Price;
            existingGame.ReleaseDate = gameDto.ReleaseDate;
            existingGame.Description = gameDto.Description;
            existingGame.LastUpdatedBy = currentUserId;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithParameterValidation()
        .DisableAntiforgery();
    }
}
