using System.ComponentModel.DataAnnotations;
using Musicana.Api.Validation;

namespace Musicana.Api.Requests;

public class AssignSongDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid MusicianId")]
    public int MusicianId { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid SongId")]
    [UniqueSongTitlePerMusician]
    public int SongId { get; set; }
}