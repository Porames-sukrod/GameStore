using System;
using GameStore.Api.Dtos;
using GameStore.Api.Entites;

namespace GameStore.Api.Mapping;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto game)
    {
        return new Game
        {
            Name = game.Name,
            GenerId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate.ToDateTime(TimeOnly.MinValue)
        };
    }

    public static GameSummaryDto ToGameSummaryDto(this Game game)
    {
        return new GameSummaryDto(
            game.Id,
            game.Name,
            game.Gener!.Name,
            game.Price,
            DateOnly.FromDateTime(game.ReleaseDate)
        );
    }

    public static GameDetailsDto ToGameDetailsDto(this Game game)
    {
        return new GameDetailsDto(
            game.Id,
            game.Name,
            game.Gener!.Name,
            game.Price,
            DateOnly.FromDateTime(game.ReleaseDate)
        );
    }

    public static Game ToEntity(this UpdateGameDto game, int id)
    {
        return new Game
        {
            Id = game.Id,
            Name = game.Name,
            GenerId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate.ToDateTime(TimeOnly.MinValue)
        };
    }

}
