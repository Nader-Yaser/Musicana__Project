using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Musicana.Api.Enums;
using Musicana.Api.Validation;

namespace Musicana.Api.Requests;

public class EditMusicianDto
{
    [Required]
    [StringLength(60, MinimumLength = 3)]
    [DisplayName("Musician Name")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
    public string Name { get; set; }

    [EnumDataType(typeof(MusicianGenre), ErrorMessage = "Invalid Musician genre")]
    public MusicianGenre? Genre { get; set; }
    [Required]
    [DataType(DataType.Date)]
    [Date]
    public DateTime BirthDate { get; set; }
}