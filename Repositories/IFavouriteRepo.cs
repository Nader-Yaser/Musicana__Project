using Musicana.Api.Models;

namespace Musicana.Api.Repositories;

public interface IFavouriteRepo
{
    Task<Favourite?> GetFavouriteByIdAsync(int favouriteId);
    Task<Favourite?> GetDefaultFavouriteAsync();
    Task AddSongToFavouriteAsync(Favourite_Song favouriteSong);
    Task RemoveSongFromFavourite(Favourite_Song favouriteSong);
    Task<bool> IsSongInFavouriteAsync(int favouriteId, int songId);
    Task SaveChanges();
}
