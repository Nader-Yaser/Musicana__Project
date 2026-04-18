using Musicana.Api.Models;
using Musicana.Api.Repositories;
using Musicana.Api.Responses;

namespace Musicana.Api.Services;

public class FavouriteService : IFavouriteService
{
    private readonly IFavouriteRepo _favouriteRepo;
    private readonly ISongRepo _songRepo;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FavouriteService(IFavouriteRepo favouriteRepo, ISongRepo songRepo,
        IHttpContextAccessor httpContextAccessor)
    {
        _favouriteRepo = favouriteRepo;
        _songRepo = songRepo;
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpRequest Request => _httpContextAccessor.HttpContext!.Request;

    public async Task<IEnumerable<FavouriteResponse>> GetAllFavouritesAsync()
    {
        var favourites = await _favouriteRepo.GetAllFavouritesAsync();
        return FavouriteResponse.FromModels(favourites, Request);
    }

    public async Task AddToFavouritesAsync(int songId)
    {
        if (songId <= 0)
            throw new ArgumentException("Invalid Song Id");

        var song = await _songRepo.GetSongByIdAsync(songId);
        if (song is null)
            throw new Exception("Song not found");

        var alreadyFavourite = await _favouriteRepo.IsFavouriteAsync(songId);
        if (alreadyFavourite)
            throw new Exception("Song is already in favourites");

        var favourite = new Favourite
        {
            SongId = songId,
            AddedAt = DateTime.UtcNow
        };

        await _favouriteRepo.AddFavouriteAsync(favourite);
        await _favouriteRepo.SaveChanges();
    }

    public async Task RemoveFromFavouritesAsync(int songId)
    {
        if (songId <= 0)
            throw new ArgumentException("Invalid Song Id");

        var favourite = await _favouriteRepo.GetFavouriteBySongIdAsync(songId);
        if (favourite is null)
            throw new Exception("Song is not in favourites");

        await _favouriteRepo.RemoveFavourite(favourite);
        await _favouriteRepo.SaveChanges();
    }

    public async Task<bool> IsFavouriteAsync(int songId)
    {
        return await _favouriteRepo.IsFavouriteAsync(songId);
    }
}
