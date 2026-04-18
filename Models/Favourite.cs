namespace Musicana.Api.Models;

public class Favourite
{
    public int Id { get; set; }
    public DateTime AddedAt { get; set; }

    // FK — كل Favourite مرتبط بأغنية واحدة
    public int SongId { get; set; }
    public Song Song { get; set; }
}
