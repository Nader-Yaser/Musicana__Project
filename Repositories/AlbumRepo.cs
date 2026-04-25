using Microsoft.EntityFrameworkCore;
using Musicana.Api.Data;
using Musicana.Api.Models;

namespace Musicana.Api.Repositories;

public class AlbumRepo : IAlbumRepo
{
    private readonly MusicanaDbContext _context;
    public AlbumRepo(MusicanaDbContext context) => _context = context;

    public async Task AddAlbumAsync(Album album)
    {
        await _context.Albums.AddAsync(album);
    }

    public async Task<Album?> GetAlbumByIdAsync(int albumId)
    {
        return await _context.Albums
            .Include(a => a.Musician)
            .Include(a => a.Songs)
            .FirstOrDefaultAsync(a => a.Id == albumId);
    }

    public async Task<IEnumerable<Album>> GetAlbumsAsync()
    {
        return await _context.Albums
            .Include(a => a.Musician)
            .Include(a => a.Songs)
            .ToListAsync();
    }

    public async Task<IEnumerable<Album>> GetAlbumsByTitleAsync(string title)
    {
        return await _context.Albums
            .Include(a => a.Musician)
            .Include(a => a.Songs)
            .Where(a => a.Title.ToLower().Contains(title.ToLower()))
            .ToListAsync();
    }

    public async Task<bool> AlbumExistsAsync(int id) => await _context.Albums.AnyAsync(a => a.Id == id);
    public async Task SaveChanges() => await _context.SaveChangesAsync();
}
