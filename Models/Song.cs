using Musicana.Api.Enums;

namespace Musicana.Api.Models;

public class Song
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public SongGenres Genre { get; set; }
    public double Duration { get; set; }
    public string FilePath { get; set; }
    public bool IsDeleted { get; set; }
    public string? CoverImagePath { get; set; }
    public int PlayCount { get; private set; }
    // Nullable FK — Song can exist without Album
    public int? AlbumId { get; set; }
    public Album? Album { get; set; }

    public List<Musician_Song> musician_Songs { get; set; }
    public List<Playlist_Song> playlist_Songs { get; set; }
    public Favourite? Favourite { get; set; }
    public void IncrementPlayCount() => PlayCount++;
}