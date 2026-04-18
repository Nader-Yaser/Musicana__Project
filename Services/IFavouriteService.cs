using Musicana.Api.Responses;

namespace Musicana.Api.Services;

public interface IFavouriteService
{
    Task<IEnumerable<FavouriteResponse>> GetAllFavouritesAsync();
    Task AddToFavouritesAsync(int songId);
    Task RemoveFromFavouritesAsync(int songId);
    Task<bool> IsFavouriteAsync(int songId);
}
