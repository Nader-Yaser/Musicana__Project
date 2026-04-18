using Musicana.Api.Responses;

namespace Musicana.Api.Services;

public interface IFavouriteService
{
    Task<FavouriteResponse?> GetFavouritesAsync();
    Task AddSongToFavouritesAsync(int songId);
    Task RemoveSongFromFavouritesAsync(int songId);
    Task<bool> IsFavouriteAsync(int songId);
}
