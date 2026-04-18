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

    public async Task<FavouriteResponse?> GetFavouritesAsync()
    {
        var favourite = await _favouriteRepo.GetDefaultFavouriteAsync();
        if (favourite is null)
            throw new Exception("Favourite list not found");

        return FavouriteResponse.FromModel(favourite, Request);
    }

    public async Task AddSongToFavouritesAsync(int songId)
    {
        if (songId <= 0)
            throw new ArgumentException("Invalid Song Id");

        var song = await _songRepo.GetSongByIdAsync(songId);
        if (song is null)
            throw new Exception("Song not found");

        var favourite = await _favouriteRepo.GetDefaultFavouriteAsync();
        if (favourite is null)
            throw new Exception("Favourite list not found");

        var alreadyExists = await _favouriteRepo.IsSongInFavouriteAsync(favourite.Id, songId);
        if (alreadyExists)
            throw new Exception("Song is already in favourites");

        var favouriteSong = new Favourite_Song
        {
            FavouriteId = favourite.Id,
            SongId = songId,
            AddedAt = DateTime.UtcNow
        };

        await _favouriteRepo.AddSongToFavouriteAsync(favouriteSong);
        await _favouriteRepo.SaveChanges();
    }

    public async Task RemoveSongFromFavouritesAsync(int songId)
    {
        if (songId <= 0)
            throw new ArgumentException("Invalid Song Id");

        var favourite = await _favouriteRepo.GetDefaultFavouriteAsync();
        if (favourite is null)
            throw new Exception("Favourite list not found");

        var favouriteSong = favourite.favourite_Songs
            .FirstOrDefault(fs => fs.SongId == songId);
        if (favouriteSong is null)
            throw new Exception("Song is not in favourites");

        await _favouriteRepo.RemoveSongFromFavourite(favouriteSong);
        await _favouriteRepo.SaveChanges();
    }

    public async Task<bool> IsFavouriteAsync(int songId)
    {
        var favourite = await _favouriteRepo.GetDefaultFavouriteAsync();
        if (favourite is null) return false;

        return await _favouriteRepo.IsSongInFavouriteAsync(favourite.Id, songId);
    }
}
