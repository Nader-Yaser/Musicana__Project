using Musicana.Api.Models;
using Musicana.Api.Repositories;
using Musicana.Api.Requests;

namespace Musicana.Api.Services;

public class Musician_SongService : IMusician_SongService
{
    private readonly IMusicianSongRepo _musicianSongRepo;
    private readonly ISongRepo _songRepo;
    private readonly IMusicianRepo _musicianRepo;

    public Musician_SongService(IMusicianSongRepo musicianSongRepo
        , IMusicianRepo musicianRepo, ISongRepo songRepo)
    {
        _musicianSongRepo = musicianSongRepo;
        _songRepo = songRepo;
        _musicianRepo = musicianRepo;
    }
    public async Task AssignSongToMusicianAsync(AssignSongDto dto)
    {
        var musician = (await _musicianRepo.GetMusicianByIdAsync(dto.MusicianId));
        if (musician is null)
            throw new Exception("Musician not found");

        var song = await _songRepo.GetSongByIdAsync(dto.SongId);
        if (song is null)
            throw new Exception("Song not found");

        var alreadyAssigned = musician.musician_Songs.Any(ms => ms.SongId == dto.SongId);
        if (alreadyAssigned)
            throw new Exception("Song already assigned to this musician");

        var musicianSong = new Musician_Song
        {
            MusicianId = dto.MusicianId,
            SongId = dto.SongId
        };

        await _musicianSongRepo.AddSongToMusicianAsync(musicianSong);
        await _musicianSongRepo.SaveAsync();
    }
}