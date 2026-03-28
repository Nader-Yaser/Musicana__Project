using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Musicana.Api.Enums;
using Musicana.Api.Validation;

namespace Musicana.Api.Requests;

public class CreateMusicianDto
{
    [Required(ErrorMessage = "Name is required")]
    [DisplayName("Musician Name")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Genre is required")]
    [DisplayName("Musician Genre")]
    [EnumDataType(typeof(MusicianGenre), ErrorMessage = "Invalid Musician genre")]
    public MusicianGenre Genre { get; set; }
    [Required(ErrorMessage = "BirthDate is required")]
    [DataType(DataType.Date)]
    [Date]
    public DateTime BirthDate { get; set; }
}