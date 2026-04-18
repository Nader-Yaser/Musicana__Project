namespace Musicana.Api.Models;

public class Favourite
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }

    // Many-to-Many with Songs
    public List<Favourite_Song> favourite_Songs { get; set; }
}
