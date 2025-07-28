using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<Genre> generes =
[
    new Genre { Id = new Guid("8c9da091-efa4-4a28-a854-1a83a3a9fa84"), Name = "Fighting"},
    new Genre { Id = new Guid("02f49762-0a06-414d-8f5a-f8d8886d3f3e"), Name = "Kids and Family"},
    new Genre { Id = new Guid("bced8c13-2da8-444d-95d4-8cba7e56510d"), Name = "Racing"},
    new Genre { Id = new Guid("92b20d4f-590f-4b1d-9a82-e380d41038e2"), Name = "Roleplaying"},
    new Genre { Id = new Guid("10dadd46-02c8-45fc-a245-b731e3b25a13"), Name = "Sport"}
];

List<Game> games =
[
    new Game {
        Id = Guid.NewGuid(),
        Name = "Street Fighter II",
        Genre = generes[0],
        Price = 19.99m,
        ReleaseDate = new DateOnly(1992, 7, 15),
        Description = "Street Fighter II is a 2D arcade fighting game where players battle as global martial artists using unique special moves."
    },
    new Game {
        Id = Guid.NewGuid(),
        Name = "Final Fantasy XIV",
        Genre = generes[3],
        Price = 59.99m,
        ReleaseDate = new DateOnly(2010, 9, 30),
        Description = "Final Fantasy XIV is a massively multiplayer online role-playing game (MMORPG) set in a rich fantasy world with epic quests and real-time combat."
    },
    new Game {
        Id = Guid.NewGuid(),
        Name = "FIFA 23",
        Genre = generes[4],
        Price = 69.99m,
        ReleaseDate = new DateOnly(2022, 9, 27),
        Description = "FIFA 23 is a football simulation game featuring realistic gameplay, licensed teams, and various competitive modes."
    }
];

// GET /games
app.MapGet("/games", () => games);

// GET /games/122233-434d-43434....
app.MapGet("/games/{id}", (Guid id) =>
{
    Game? game = games.Find(game => game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);
})
.WithName(GetGameEndpointName);

// POST /games
app.MapPost("/games", (Game game) =>
{
    game.Id = Guid.NewGuid();
    games.Add(game);

    return Results.CreatedAtRoute(
        GetGameEndpointName,
        new { id = game.Id },
        game);
})
.WithParameterValidation();

// PUT /games/122233-434d-43434....
app.MapPut("/games/{id}", (Guid id, Game updatedGame) =>
{
    var existingGame = games.Find(game => game.Id == id);

    if (existingGame is null)
    {
        return Results.NotFound();
    }

    existingGame.Name = updatedGame.Name;
    existingGame.Genre = updatedGame.Genre;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;

    return Results.NoContent();
})
.WithParameterValidation();

// DELETE /games/122233-434d-43434....
app.MapDelete("/games/{id}", (Guid id) =>
{
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});


app.Run();
