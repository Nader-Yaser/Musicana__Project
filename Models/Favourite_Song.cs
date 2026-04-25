namespace Musicana.Api.Models;

public class Favourite_Song
{
    public int FavouriteId { get; set; }
    public int SongId { get; set; }
    public DateTime AddedAt { get; set; } 

    public Favourite Favourite { get; set; }
    public Song Song { get; set; }
}
