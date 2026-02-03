using Microsoft.AspNetCore.Http.Features;

namespace GameStore.Api.Features.Baskets.GetBasket;

public record class BasketDto(
    Guid CustomerId,
    IEnumerable<BasketItemDto> Items
)
{
    public decimal TotalAmount => Items.Sum(ItemsFeature => ItemsFeature.Price * ItemsFeature.Quantity);
}

public record class BasketItemDto(
    Guid Id,
    string Name,
    decimal Price,
    int Quantity,
    string ImageUri
);
