using Musicana.Api.Models;

namespace Musicana.Api.Repositories;

public interface IFavouriteRepo
{
    Task<IEnumerable<Favourite>> GetAllFavouritesAsync();
    Task<Favourite?> GetFavouriteBySongIdAsync(int songId);
    Task AddFavouriteAsync(Favourite favourite);
    Task RemoveFavourite(Favourite favourite);
    Task<bool> IsFavouriteAsync(int songId);
    Task SaveChanges();
}
