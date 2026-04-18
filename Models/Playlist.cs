namespace Musicana.Api.Models;

public class Playlist
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? CoverImagePath { get; set; }
    public bool IsDeleted { get; set; }

    // Many-to-Many with Songs
    public List<Playlist_Song> playlist_Songs { get; set; }
}
