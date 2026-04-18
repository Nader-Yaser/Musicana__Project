using Microsoft.EntityFrameworkCore;
using Musicana.Api.Data;
using Musicana.Api.Models;

namespace Musicana.Api.Repositories;

public class FavouriteRepo : IFavouriteRepo
{
    private readonly MusicanaDbContext _context;
    public FavouriteRepo(MusicanaDbContext context) => _context = context;

    public async Task<Favourite?> GetFavouriteByIdAsync(int favouriteId)
    {
        return await _context.Favourites
            .Include(f => f.favourite_Songs)
            .ThenInclude(fs => fs.Song)
            .FirstOrDefaultAsync(f => f.Id == favouriteId);
    }

    public async Task<Favourite?> GetDefaultFavouriteAsync()
    {
        return await _context.Favourites
            .Include(f => f.favourite_Songs)
            .ThenInclude(fs => fs.Song)
            .FirstOrDefaultAsync();
    }

    public async Task AddSongToFavouriteAsync(Favourite_Song favouriteSong)
    {
        await _context.Favourite_Songs.AddAsync(favouriteSong);
    }

    public Task RemoveSongFromFavourite(Favourite_Song favouriteSong)
    {
        _context.Favourite_Songs.Remove(favouriteSong);
        return Task.CompletedTask;
    }

    public async Task<bool> IsSongInFavouriteAsync(int favouriteId, int songId)
    {
        return await _context.Favourite_Songs
            .AnyAsync(fs => fs.FavouriteId == favouriteId && fs.SongId == songId);
    }

    public async Task SaveChanges() => await _context.SaveChangesAsync();
}
