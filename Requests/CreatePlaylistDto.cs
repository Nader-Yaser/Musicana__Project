using System.ComponentModel.DataAnnotations;
using Musicana.Api.Validation;

namespace Musicana.Api.Requests;

public class CreatePlaylistDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string Name { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }

    [DataType(DataType.Upload)]
    [AllowedImageExtensions]
    public IFormFile? CoverImage { get; set; }
}
