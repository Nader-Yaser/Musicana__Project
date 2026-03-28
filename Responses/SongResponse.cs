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
    public List<string> musicians { get; set; } = new List<string>();
    private SongResponse() { }

    public static SongResponse FromModel(Song song, HttpRequest request)
    {

        var scheme = request.Headers.ContainsKey("X-Forwarded-Proto")
        ? request.Headers["X-Forwarded-Proto"].ToString()
        : request.Scheme;

        var host = request.Headers.ContainsKey("X-Forwarded-Host")
            ? request.Headers["X-Forwarded-Host"].ToString()
            : request.Host.ToString();

        return new SongResponse
        {
            SongId = song.Id,
            Title = song.Title,
            Genre = song.Genre,
            Duration = song.Duration,
            Description = song.Description,
            FileUrl = $"{scheme}://{host}{song.FilePath}",
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