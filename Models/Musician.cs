using Musicana.Api.Enums;

namespace Musicana.Api.Models;

public class Musician
{
    public int Id { get; set; }
    public string Name { get; set; }
    public MusicianGenre? Genre { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsDeleted { get; set; }
    public List<Musician_Song> musician_Songs { get; set; }
    public List<Musician_Instrument> musician_Instruments { get; set; }

    public List<Album> Albums { get; set; }
}