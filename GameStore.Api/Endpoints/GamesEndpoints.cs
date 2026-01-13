using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Api.Dtos;
namespace GameStore.Api.Endpoints
{
    public static class GamesEndpoints
    {
        const string GetGameEndpointName = "GetGame";
        private static readonly List<GameDto> games = new List<GameDto>
        {
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
        };

        public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("games").WithParameterValidation();

            //get /games
            group.MapGet("/", () => games);

            //get /games/{id}
            group.MapGet("/{id}", (int id) =>
            {
                GameDto? game = games.Find(game => game.Id == id);

                return game is null
                    ? Results.NotFound()
                    : Results.Ok(game);
            })
            .WithName(GetGameEndpointName);

            //post /games
            group.MapPost("/", (CreateGameDto newGame) =>
            {
                GameDto game = new(
                    games.Count + 1,
                    newGame.Name,
                    newGame.Genre,
                    newGame.Price,
                    newGame.ReleaseDate
                );
                games.Add(game);
                return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
            });
            

            //put /games
            group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
            {
                
                var gameIndex = games.FindIndex(game => game.Id == id);

                if (gameIndex == -1)
                {
                    return Results.NotFound();
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

            //delete /games/{id}
            group.MapDelete("/{id}", (int id) =>
            {
                games.RemoveAll(game => game.Id == id);

                return Results.NoContent();
            });
            return group;
        }
    }
}