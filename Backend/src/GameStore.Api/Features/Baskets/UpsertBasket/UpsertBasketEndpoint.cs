using GameStore.Api.Data;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Baskets.UpsertBasket;

public static class UpsertBasketEndpoint
{
    public static void MapUpsertBasket(this IEndpointRouteBuilder app)
    {
        // PUT /baskets/48b1aea1-7089-4c38-8512-cb00732fdc19
        app.MapPut("/{userid}", async (
            Guid userid,
            UpsertBasketDto upsertBasketDtos,
            GameStoreContext dbContext
        ) =>
        {
            CustomerBasket? basket = await dbContext.Baskets
                                        .Include(basket => basket.Items)
                                        .FirstOrDefaultAsync(
                                            basket => basket.Id == userid);
            if (basket is null)
            {
                basket = new CustomerBasket
                {
                    Id = userid,
                    Items = upsertBasketDtos.Items.Select(item => new BasketItem
                    {
                        GameId = item.Id,
                        Quantity = item.Quantity
                    }).ToList()
                };

                dbContext.Baskets.Add(basket);
            }
            else
            {
                basket.Items = upsertBasketDtos.Items.Select(item => new BasketItem
                {
                    GameId = item.Id,
                    Quantity = item.Quantity
                }).ToList();
            }

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}
