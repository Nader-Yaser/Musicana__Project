using Musicana.Api.Models;

namespace Musicana.Api.Responses;

public class PlaylistResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? CoverImageUrl { get; set; }
    public List<string> Songs { get; set; } = new List<string>();

    private PlaylistResponse() { }

    public static PlaylistResponse FromModel(Playlist playlist, HttpRequest request)
    {
        return new PlaylistResponse
        {
            Id = playlist.Id,
            Name = playlist.Name,
            Description = playlist.Description,
            CoverImageUrl = playlist.CoverImagePath != null
                ? $"{request.Scheme}://{request.Host}{playlist.CoverImagePath}"
                : null,
            Songs = playlist.playlist_Songs?
                .OrderBy(ps => ps.Order)
                .Select(ps => ps.Song.Title)
                .ToList() ?? new List<string>()
        };
    }

    public static IEnumerable<PlaylistResponse> FromModels(IEnumerable<Playlist> playlists, HttpRequest request)
    {
        if (playlists is null)
            throw new ArgumentNullException(nameof(playlists));

        return playlists.Select(p => FromModel(p, request));
    }
}
