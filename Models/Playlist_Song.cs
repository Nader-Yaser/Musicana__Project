namespace Musicana.Api.Models;

public class Playlist_Song
{
    public int PlaylistId { get; set; }
    public int SongId { get; set; }
    public int Order { get; set; }  // ترتيب الأغنية في الـ Playlist

    public Playlist Playlist { get; set; }
    public Song Song { get; set; }
}
