using Musicana.Api.Enums;
using Musicana.Api.Models;

namespace Musicana.Api.Repositories;

public interface ISongRepo
{
    Task<IEnumerable<Song>> GetSongsAsync();
    Task<Song?> GetSongByIdAsync(int songId);
    Task<IEnumerable<Song>> GetSongByTitleAsync(string title);
    Task<IEnumerable<Song>> GetSongsByGenreAsync(SongGenres genre);
    Task AddSongAsync(Song song);
    Task<bool> SongExistsAsync(int id);
    Task SaveChanges();
}