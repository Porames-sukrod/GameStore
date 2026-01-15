using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos
{
    public record CreateGameDto(
        [Required][StringLength(100)]string Name,
        int GenreId,
        [Range(0,  100)]decimal Price,
        DateOnly ReleaseDate
    );
}