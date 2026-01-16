using System;
using System.Text.RegularExpressions;
using GameStore.Api.Data;
using GameStore.Api.Entites;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Dtos;

public static class GenresEndpoints
{
    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var routeGroupBuilder = app.MapGroup("genres");

        routeGroupBuilder.MapGet("/", async(GameStoreContext dbContext)=>
            await dbContext.Genres
                .Select(Gener => Gener.ToDto())
                .AsNoTracking()
                .ToListAsync()
        );
        
        return routeGroupBuilder;
    }
}
