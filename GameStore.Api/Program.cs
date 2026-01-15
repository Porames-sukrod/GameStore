using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStoreConnection");
builder.Services.AddSqlite<GameStore.Api.Data.GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndpoints();

await app.MigrateDbAsync();

app.Run();