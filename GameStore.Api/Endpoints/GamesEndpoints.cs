using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entites;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;
namespace GameStore.Api.Endpoints
{
    public static class GamesEndpoints
    {
        const string GetGameEndpointName = "GetGame";

        public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("games").WithParameterValidation();

            //get /games
            group.MapGet("/", async (GameStoreContext dbContext) => 
            await dbContext.Games
                .Include(game => game.Gener)
                .Select(game => game.ToGameSummaryDto())
                .AsNoTracking()
                .ToListAsync());
                
            //get /games/{id}
            group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
            {
                Game? game = await dbContext.Games.FindAsync(id);
                    

                return game is null
                    ? Results.NotFound()
                    : Results.Ok(game.ToGameDetailsDto());
            })
            .WithName(GetGameEndpointName);

            //post /games
            _ = group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
            {
                Game game = newGame.ToEntity();
                

                dbContext.Games.Add(game);
                await dbContext.SaveChangesAsync();
                return Results.CreatedAtRoute(
                    GetGameEndpointName, 
                    new { id = game.Id }, 
                    game.ToGameDetailsDto());
            });


            //put /games
            group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
            {
                var existingGame = await dbContext.Games.FindAsync(id);

                if (existingGame is null)
                {
                    return Results.NotFound();
                }

                existingGame.Name = updatedGame.Name;
                existingGame.GenerId = updatedGame.GenreId;
                existingGame.Price = updatedGame.Price;
                existingGame.ReleaseDate = updatedGame.ReleaseDate.ToDateTime(TimeOnly.MinValue);

                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            });

            //delete /games/{id}
            group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
            {
                var game = await dbContext.Games.FindAsync(id);
                
                if (game is null)
                {
                    return Results.NotFound();
                }

                dbContext.Games.Remove(game);
                dbContext.SaveChanges();
                return Results.NoContent();
            });
            return group;
        }

        private static void AsNoTracking()
        {
            throw new NotImplementedException();
        }
    }
}