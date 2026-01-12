namespace GameStore.Api.Dtos;

public record class createDameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
