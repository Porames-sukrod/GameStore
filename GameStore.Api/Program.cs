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
app.MapPost("/games" , (createDameDto newGame) =>
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

app.Run();