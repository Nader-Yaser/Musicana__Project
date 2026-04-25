using Microsoft.EntityFrameworkCore;
using Musicana.Api.Data;
using Musicana.Api.Models;

namespace Musicana.Api.Repositories;

public class PlaylistRepo : IPlaylistRepo
{
    private readonly MusicanaDbContext _context;
    public PlaylistRepo(MusicanaDbContext context) => _context = context;

    public async Task AddPlaylistAsync(Playlist playlist)
    {
        await _context.Playlists.AddAsync(playlist);
    }

    public async Task<Playlist?> GetPlaylistByIdAsync(int playlistId)
    {
        return await _context.Playlists
            .Include(p => p.playlist_Songs)
            .ThenInclude(ps => ps.Song)
            .FirstOrDefaultAsync(p => p.Id == playlistId);
    }

    public async Task<IEnumerable<Playlist>> GetPlaylistsAsync()
    {
        return await _context.Playlists
            .Include(p => p.playlist_Songs)
            .ThenInclude(ps => ps.Song)
            .ToListAsync();
    }

    public async Task<IEnumerable<Playlist>> GetPlaylistsByNameAsync(string name)
    {
        return await _context.Playlists
            .Include(p => p.playlist_Songs)
            .ThenInclude(ps => ps.Song)
            .Where(p => p.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    }

    public async Task<bool> PlaylistExistsAsync(int id) => await _context.Playlists.AnyAsync(p => p.Id == id);
    public async Task SaveChanges() => await _context.SaveChangesAsync();
}
