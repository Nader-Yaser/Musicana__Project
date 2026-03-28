using System.ComponentModel.DataAnnotations;

namespace Musicana.Api.Validation;

public class AllowedAudioExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions = { ".mp3", ".wav", ".flac", ".aac" };

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success;

        if (value is not IFormFile file)
            return new ValidationResult("Invalid file");

        var extension = Path.GetExtension(file.FileName).ToLower();
        if (!_extensions.Contains(extension))
            return new ValidationResult("File must be mp3, wav, flac, or aac");

        if (file.Length > 50 * 1024 * 1024)
            return new ValidationResult("File size cannot exceed 50MB");

        if (file.Length == 0)
            return new ValidationResult("File cannot be empty");

        return ValidationResult.Success;
    }
}