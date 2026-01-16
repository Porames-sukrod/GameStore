using System;
using GameStore.Api.Entites;

namespace GameStore.Api.Dtos;

public static class GenereMapping
{
    public static GenreDto ToDto(this Gener genere)
    {
        return new GenreDto(genere.Id, genere.Name);
    }
}
