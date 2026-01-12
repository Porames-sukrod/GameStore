using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndopointName = "GetGame";

List<GameDto> games = [
    new (
        1,
        "The Legend of Code",
        "Adventure",
        59.99m,
        new DateOnly(2023, 11, 15)
    ),
    new (
        2,
        "Debugging Quest",
        "RPG",
        49.99m,
        new DateOnly(2024, 2, 20)
    ),
    new (
        3,
        "Refactor Racer",
        "Racing",
        39.99m,
        new DateOnly(2022, 8, 5)
    )
];

//get /game
app.MapGet("/games", () => games);

//get /game/{id}
app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id))
 .WithName(GetGameEndopointName);

//post /games
app.MapPost("/games", (CreateGameDto newGame) =>
{
    GameDto game = new (
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndopointName, new { id = game.Id }, game);
});

//put /games
app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) =>
{
    var gameIndex = games.FindIndex(game => game.Id == id);

    if (gameIndex == -1) 
    {
        return Results.NotFound(); // ถ้าหาไม่เจอ ให้บอกว่าไม่พบ (404)
    }

    games[gameIndex] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );
    return Results.NoContent();
});


app.Run();