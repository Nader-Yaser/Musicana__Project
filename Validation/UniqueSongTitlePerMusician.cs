using System.ComponentModel.DataAnnotations;
using Musicana.Api.Data;
using Musicana.Api.Requests;
using Musicana.Api.Responses;

namespace Musicana.Api.Validation;

public class UniqueSongTitlePerMusicianAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var _context = validationContext.GetService(typeof(MusicanaDbContext)) as MusicanaDbContext;
        if (_context is null)
            return new ValidationResult("Database context not available");

        var dto = validationContext.ObjectInstance as AssignSongDto;
        if (dto is null)
            return new ValidationResult("Invalid object type");

        var song = _context.Songs.FirstOrDefault(s => s.Id == dto.SongId);
        if (song is null)
            return new ValidationResult("Song not found");

        var exists = _context.Musician_Songs
            .Any(ms => ms.Song.Title.ToLower() == song.Title.ToLower()
                    && ms.MusicianId == dto.MusicianId);

        if (exists)
            return new ValidationResult("This musician already has a song with the same title");

        return ValidationResult.Success;
    }
}