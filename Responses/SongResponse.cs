using Musicana.Api.Enums;
using Musicana.Api.Models;

namespace Musicana.Api.Responses;

public class SongResponse
{
    public int SongId { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public double Duration { get; set; }
    public SongGenres Genre { get; set; }
    public string FileUrl { get; set; }
    public string CoverImageUrl { get; set; }
    public List<string> musicians { get; set; } = new List<string>();
    private SongResponse() { }

    public static SongResponse FromModel(Song song, HttpRequest request)
    {



        return new SongResponse
        {
            SongId = song.Id,
            Title = song.Title,
            Genre = song.Genre,
            Duration = song.Duration,
            Description = song.Description,
            FileUrl = $"{request.Scheme}://{request.Host}{song.FilePath}",
            CoverImageUrl = $"{request.Scheme}://{request.Host}{song.CoverImagePath}",
            musicians = song.musician_Songs?
            .Select(ms => ms.Musician.Name)
            .ToList() ?? new List<string>()
        };
    }

    public static IEnumerable<SongResponse> FromModels(IEnumerable<Song> songs, HttpRequest request)
    {
        if (songs is null)
            throw new ArgumentNullException(nameof(songs));

        return songs.Select(s => FromModel(s, request));
    }
}