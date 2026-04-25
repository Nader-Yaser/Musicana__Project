using Musicana.Api.Models;

namespace Musicana.Api.Responses;

public class AlbumResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? CoverImageUrl { get; set; }
    public string MusicianName { get; set; }
    public List<string> Songs { get; set; } = new List<string>();

    private AlbumResponse() { }

    public static AlbumResponse FromModel(Album album, HttpRequest request)
    {
        return new AlbumResponse
        {
            Id = album.Id,
            Title = album.Title,
            Description = album.Description,
            ReleaseDate = album.ReleaseDate,
            CoverImageUrl = album.CoverImagePath != null
                ? $"{request.Scheme}://{request.Host}{album.CoverImagePath}"
                : null,
            MusicianName = album.Musician?.Name ?? "",
            Songs = album.Songs?
                .Select(s => s.Title)
                .ToList() ?? new List<string>()
        };
    }

    public static IEnumerable<AlbumResponse> FromModels(IEnumerable<Album> albums, HttpRequest request)
    {
        if (albums is null)
            throw new ArgumentNullException(nameof(albums));

        return albums.Select(a => FromModel(a, request));
    }
}
