using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace GameStore.Api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options)
    : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();

    public DbSet<CustomerBasket> Baskets => Set<CustomerBasket>();

    public DbSet<BasketItem> BasketItems => Set<BasketItem>();
}
