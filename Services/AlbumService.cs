using Musicana.Api.Models;
using Musicana.Api.Repositories;
using Musicana.Api.Requests;
using Musicana.Api.Responses;

namespace Musicana.Api.Services;

public class AlbumService : IAlbumService
{
    private readonly IAlbumRepo _albumRepo;
    private readonly ISongRepo _songRepo;
    private readonly IMusicianRepo _musicianRepo;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AlbumService(IAlbumRepo albumRepo, ISongRepo songRepo,
        IMusicianRepo musicianRepo, IHttpContextAccessor httpContextAccessor)
    {
        _albumRepo = albumRepo;
        _songRepo = songRepo;
        _musicianRepo = musicianRepo;
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpRequest Request => _httpContextAccessor.HttpContext!.Request;

    public async Task CreateAlbumAsync(CreateAlbumDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));

        var musician = await _musicianRepo.GetMusicianByIdAsync(dto.MusicianId);
        if (musician is null)
            throw new Exception("Musician not found");

        var album = new Album
        {
            Title = dto.Title,
            Description = dto.Description,
            ReleaseDate = dto.ReleaseDate,
            MusicianId = dto.MusicianId,
            IsDeleted = false,
            CoverImagePath = dto.CoverImage != null ? await SaveCoverImageAsync(dto.CoverImage) : null
        };

        await _albumRepo.AddAlbumAsync(album);
        await _albumRepo.SaveChanges();
    }

    public async Task DeleteAlbumAsync(int albumId)
    {
        if (albumId <= 0)
            throw new ArgumentException("Invalid Id");

        var album = await _albumRepo.GetAlbumByIdAsync(albumId);
        if (album is null)
            throw new Exception("Album not found");

        album.IsDeleted = true;
        await _albumRepo.SaveChanges();
    }

    public async Task<IEnumerable<AlbumResponse>> GetAlbumsAsync()
    {
        var albums = await _albumRepo.GetAlbumsAsync();
        return AlbumResponse.FromModels(albums, Request);
    }

    public async Task<AlbumResponse?> GetAlbumByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid id");

        var album = await _albumRepo.GetAlbumByIdAsync(id);
        if (album is null)
            throw new Exception("Album not found");

        return AlbumResponse.FromModel(album, Request);
    }

    public async Task<IEnumerable<AlbumResponse>> GetAlbumsByTitleAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required");

        var albums = await _albumRepo.GetAlbumsByTitleAsync(title);
        return AlbumResponse.FromModels(albums, Request);
    }

    public async Task UpdateAlbumAsync(int id, EditAlbumDto dto)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid Id");

        var album = await _albumRepo.GetAlbumByIdAsync(id);
        if (album is null)
            throw new Exception("Album not found");

        if (dto.CoverImage is not null)
        {
            if (album.CoverImagePath != null)
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                    album.CoverImagePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (System.IO.File.Exists(oldFilePath))
                    System.IO.File.Delete(oldFilePath);
            }
            album.CoverImagePath = await SaveCoverImageAsync(dto.CoverImage);
        }

        album.Title = dto.Title;
        album.Description = dto.Description;
        album.ReleaseDate = dto.ReleaseDate ?? album.ReleaseDate;

        await _albumRepo.SaveChanges();
    }

    public async Task AssignSongToAlbumAsync(int albumId, int songId)
    {
        var album = await _albumRepo.GetAlbumByIdAsync(albumId);
        if (album is null)
            throw new Exception("Album not found");

        var song = await _songRepo.GetSongByIdAsync(songId);
        if (song is null)
            throw new Exception("Song not found");

        song.AlbumId = albumId;
        await _songRepo.SaveChanges();
    }

    public async Task RemoveSongFromAlbumAsync(int songId)
    {
        var song = await _songRepo.GetSongByIdAsync(songId);
        if (song is null)
            throw new Exception("Song not found");

        song.AlbumId = null;
        await _songRepo.SaveChanges();
    }

    private async Task<string> SaveCoverImageAsync(IFormFile file)
    {
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CoverImages");

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return $"/CoverImages/{uniqueFileName}";
    }
}
