using Musicana.Api.Requests;
using Musicana.Api.Responses;

namespace Musicana.Api.Services;

public interface IAlbumService
{
    Task<IEnumerable<AlbumResponse>> GetAlbumsAsync();
    Task<AlbumResponse?> GetAlbumByIdAsync(int id);
    Task<IEnumerable<AlbumResponse>> GetAlbumsByTitleAsync(string title);
    Task CreateAlbumAsync(CreateAlbumDto dto);
    Task UpdateAlbumAsync(int albumId, EditAlbumDto dto);
    Task DeleteAlbumAsync(int albumId);
    Task AssignSongToAlbumAsync(int albumId, int songId);
    Task RemoveSongFromAlbumAsync(int songId);
}
