using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos
{
    public record UpdateGameDto(
        [Required][StringLength(100)] string Name,
        int GenreId,
        [Range(0, 100)] decimal Price,
        DateOnly ReleaseDate
    )
    {
        public int Id { get; internal set; }
    }
}