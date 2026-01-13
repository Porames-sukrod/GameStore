using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos
{
    public record UpdateGameDto(
        [Required][StringLength(100)]string Name,
        [Required][StringLength(100)]string Genre,
        [Range(0,  100)]decimal Price,
        DateOnly ReleaseDate
    );
}