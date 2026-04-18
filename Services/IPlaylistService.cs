using Musicana.Api.Requests;
using Musicana.Api.Responses;

namespace Musicana.Api.Services;

public interface IPlaylistService
{
    Task<IEnumerable<PlaylistResponse>> GetPlaylistsAsync();
    Task<PlaylistResponse?> GetPlaylistByIdAsync(int id);
    Task<IEnumerable<PlaylistResponse>> GetPlaylistsByNameAsync(string name);
    Task CreatePlaylistAsync(CreatePlaylistDto dto);
    Task UpdatePlaylistAsync(int playlistId, EditPlaylistDto dto);
    Task DeletePlaylistAsync(int playlistId);
    Task AddSongToPlaylistAsync(int playlistId, int songId);
    Task RemoveSongFromPlaylistAsync(int playlistId, int songId);
}
