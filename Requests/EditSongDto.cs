using System.ComponentModel.DataAnnotations;
using Musicana.Api.Enums;
using Musicana.Api.Validation;

namespace Musicana.Api.Requests;

public class EditSongDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 200 characters")]
    public string Title { get; set; }
    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }

    [Range(0.5, 1200, ErrorMessage = "Duration must be between 0.5 and 1200 seconds")]
    public double? Duration { get; set; }

    [Required(ErrorMessage = "Genre is required")]
    [EnumDataType(typeof(SongGenres), ErrorMessage = "Invalid Song genre")]
    public SongGenres Genre { get; set; } 

    [AllowedAudioExtensions]
    public IFormFile? FormFile { get; set; } 
}