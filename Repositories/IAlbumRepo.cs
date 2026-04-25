using Musicana.Api.Models;

namespace Musicana.Api.Repositories;

public interface IAlbumRepo
{
    Task<IEnumerable<Album>> GetAlbumsAsync();
    Task<Album?> GetAlbumByIdAsync(int albumId);
    Task<IEnumerable<Album>> GetAlbumsByTitleAsync(string title);
    Task AddAlbumAsync(Album album);
    Task<bool> AlbumExistsAsync(int id);
    Task SaveChanges();
}
