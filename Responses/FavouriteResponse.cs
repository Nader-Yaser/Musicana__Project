using Musicana.Api.Enums;
using Musicana.Api.Models;

namespace Musicana.Api.Responses;

public class FavouriteResponse
{
    public int SongId { get; set; }
    public string Title { get; set; }
    public SongGenres Genre { get; set; }
    public string? CoverImageUrl { get; set; }
    public DateTime AddedAt { get; set; }

    private FavouriteResponse() { }

    public static FavouriteResponse FromModel(Favourite favourite, HttpRequest request)
    {
        return new FavouriteResponse
        {
            SongId = favourite.SongId,
            Title = favourite.Song.Title,
            Genre = favourite.Song.Genre,
            CoverImageUrl = favourite.Song.CoverImagePath != null
                ? $"{request.Scheme}://{request.Host}{favourite.Song.CoverImagePath}"
                : null,
            AddedAt = favourite.AddedAt
        };
    }

    public static IEnumerable<FavouriteResponse> FromModels(IEnumerable<Favourite> favourites, HttpRequest request)
    {
        if (favourites is null)
            throw new ArgumentNullException(nameof(favourites));

        return favourites.Select(f => FromModel(f, request));
    }
}
