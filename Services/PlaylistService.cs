using Musicana.Api.Models;
using Musicana.Api.Repositories;
using Musicana.Api.Requests;
using Musicana.Api.Responses;

namespace Musicana.Api.Services;

public class PlaylistService : IPlaylistService
{
    private readonly IPlaylistRepo _playlistRepo;
    private readonly ISongRepo _songRepo;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PlaylistService(IPlaylistRepo playlistRepo, ISongRepo songRepo,
        IHttpContextAccessor httpContextAccessor)
    {
        _playlistRepo = playlistRepo;
        _songRepo = songRepo;
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpRequest Request => _httpContextAccessor.HttpContext!.Request;

    public async Task CreatePlaylistAsync(CreatePlaylistDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));

        var playlist = new Playlist
        {
            Name = dto.Name,
            Description = dto.Description,
            IsDeleted = false,
            CoverImagePath = dto.CoverImage != null ? await SaveCoverImageAsync(dto.CoverImage) : null
        };

        await _playlistRepo.AddPlaylistAsync(playlist);
        await _playlistRepo.SaveChanges();
    }

    public async Task DeletePlaylistAsync(int playlistId)
    {
        if (playlistId <= 0)
            throw new ArgumentException("Invalid Id");

        var playlist = await _playlistRepo.GetPlaylistByIdAsync(playlistId);
        if (playlist is null)
            throw new Exception("Playlist not found");

        playlist.IsDeleted = true;
        await _playlistRepo.SaveChanges();
    }

    public async Task<IEnumerable<PlaylistResponse>> GetPlaylistsAsync()
    {
        var playlists = await _playlistRepo.GetPlaylistsAsync();
        return PlaylistResponse.FromModels(playlists, Request);
    }

    public async Task<PlaylistResponse?> GetPlaylistByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid id");

        var playlist = await _playlistRepo.GetPlaylistByIdAsync(id);
        if (playlist is null)
            throw new Exception("Playlist not found");

        return PlaylistResponse.FromModel(playlist, Request);
    }

    public async Task<IEnumerable<PlaylistResponse>> GetPlaylistsByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required");

        var playlists = await _playlistRepo.GetPlaylistsByNameAsync(name);
        return PlaylistResponse.FromModels(playlists, Request);
    }

    public async Task UpdatePlaylistAsync(int id, EditPlaylistDto dto)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid Id");

        var playlist = await _playlistRepo.GetPlaylistByIdAsync(id);
        if (playlist is null)
            throw new Exception("Playlist not found");

        if (dto.CoverImage is not null)
        {
            if (playlist.CoverImagePath != null)
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                    playlist.CoverImagePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (System.IO.File.Exists(oldFilePath))
                    System.IO.File.Delete(oldFilePath);
            }
            playlist.CoverImagePath = await SaveCoverImageAsync(dto.CoverImage);
        }

        playlist.Name = dto.Name;
        playlist.Description = dto.Description;

        await _playlistRepo.SaveChanges();
    }

    public async Task AddSongToPlaylistAsync(int playlistId, int songId)
    {
        var playlist = await _playlistRepo.GetPlaylistByIdAsync(playlistId);
        if (playlist is null)
            throw new Exception("Playlist not found");

        var song = await _songRepo.GetSongByIdAsync(songId);
        if (song is null)
            throw new Exception("Song not found");

        var alreadyExists = playlist.playlist_Songs.Any(ps => ps.SongId == songId);
        if (alreadyExists)
            throw new Exception("Song already exists in this playlist");

        var nextOrder = playlist.playlist_Songs.Any()
            ? playlist.playlist_Songs.Max(ps => ps.Order) + 1
            : 1;

        playlist.playlist_Songs.Add(new Playlist_Song
        {
            PlaylistId = playlistId,
            SongId = songId,
            Order = nextOrder
        });

        await _playlistRepo.SaveChanges();
    }

    public async Task RemoveSongFromPlaylistAsync(int playlistId, int songId)
    {
        var playlist = await _playlistRepo.GetPlaylistByIdAsync(playlistId);
        if (playlist is null)
            throw new Exception("Playlist not found");

        var playlistSong = playlist.playlist_Songs.FirstOrDefault(ps => ps.SongId == songId);
        if (playlistSong is null)
            throw new Exception("Song not found in this playlist");

        playlist.playlist_Songs.Remove(playlistSong);
        await _playlistRepo.SaveChanges();
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
