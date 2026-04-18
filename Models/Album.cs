namespace Musicana.Api.Models;

public class Album
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? CoverImagePath { get; set; }
    public bool IsDeleted { get; set; }

    public int MusicianId { get; set; }
    public Musician Musician { get; set; }

    public List<Song> Songs { get; set; }
}
