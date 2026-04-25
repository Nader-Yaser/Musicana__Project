using System.ComponentModel.DataAnnotations;
using Musicana.Api.Validation;

namespace Musicana.Api.Requests;

public class CreateAlbumDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 100 characters")]
    public string Title { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Release date is required")]
    public DateTime ReleaseDate { get; set; }

    [Required(ErrorMessage = "Musician ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid MusicianId")]
    public int MusicianId { get; set; }

    [DataType(DataType.Upload)]
    [AllowedImageExtensions]
    public IFormFile? CoverImage { get; set; }
}
