using Musicana.Api.Models;

namespace Musicana.Api.Repositories;

public interface IPlaylistRepo
{
    Task<IEnumerable<Playlist>> GetPlaylistsAsync();
    Task<Playlist?> GetPlaylistByIdAsync(int playlistId);
    Task<IEnumerable<Playlist>> GetPlaylistsByNameAsync(string name);
    Task AddPlaylistAsync(Playlist playlist);
    Task<bool> PlaylistExistsAsync(int id);
    Task SaveChanges();
}
