using Microsoft.EntityFrameworkCore;
using Musicana.Api.Data;
using Musicana.Api.Models;

namespace Musicana.Api.Repositories;

public class FavouriteRepo : IFavouriteRepo
{
    private readonly MusicanaDbContext _context;
    public FavouriteRepo(MusicanaDbContext context) => _context = context;

    public async Task<IEnumerable<Favourite>> GetAllFavouritesAsync()
    {
        return await _context.Favourites
            .Include(f => f.Song)
            .OrderByDescending(f => f.AddedAt)
            .ToListAsync();
    }

    public async Task<Favourite?> GetFavouriteBySongIdAsync(int songId)
    {
        return await _context.Favourites
            .Include(f => f.Song)
            .FirstOrDefaultAsync(f => f.SongId == songId);
    }

    public async Task AddFavouriteAsync(Favourite favourite)
    {
        await _context.Favourites.AddAsync(favourite);
    }

    public void RemoveFavourite(Favourite favourite)
    {
        _context.Favourites.Remove(favourite);
    }

    public async Task<bool> IsFavouriteAsync(int songId)
    {
        return await _context.Favourites.AnyAsync(f => f.SongId == songId);
    }

    public async Task SaveChanges() => await _context.SaveChangesAsync();

    Task IFavouriteRepo.RemoveFavourite(Favourite favourite)
    {
        _context.Favourites.Remove(favourite);
        return Task.CompletedTask;
    }
}
