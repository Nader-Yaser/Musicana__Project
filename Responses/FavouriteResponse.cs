using Musicana.Api.Enums;
using Musicana.Api.Models;

namespace Musicana.Api.Responses;

public class FavouriteResponse
{
    public int FavouriteId { get; set; }
    public List<FavouriteSongItem> Songs { get; set; } = new();

    private FavouriteResponse() { }

    public static FavouriteResponse FromModel(Favourite favourite, HttpRequest request)
    {
        return new FavouriteResponse
        {
            FavouriteId = favourite.Id,
            Songs = favourite.favourite_Songs?
                .OrderByDescending(fs => fs.AddedAt)
                .Select(fs => new FavouriteSongItem
                {
                    SongId = fs.SongId,
                    Title = fs.Song.Title,
                    Genre = fs.Song.Genre,
                    CoverImageUrl = fs.Song.CoverImagePath != null
                        ? $"{request.Scheme}://{request.Host}{fs.Song.CoverImagePath}"
                        : null,
                    AddedAt = fs.AddedAt
                }).ToList() ?? new List<FavouriteSongItem>()
        };
    }
}

public class FavouriteSongItem
{
    public int SongId { get; set; }
    public string Title { get; set; }
    public SongGenres Genre { get; set; }
    public string? CoverImageUrl { get; set; }
    public DateTime AddedAt { get; set; }
}
