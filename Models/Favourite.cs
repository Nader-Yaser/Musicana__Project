namespace Musicana.Api.Models;

public class Favourite
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<Favourite_Song> favourite_Songs { get; set; }
}
