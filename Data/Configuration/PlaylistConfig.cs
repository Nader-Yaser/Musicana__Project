using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musicana.Api.Models;

namespace Musicana.Api.Data.Configuration;

public class PlaylistConfig : IEntityTypeConfiguration<Playlist>
{
    public void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.ToTable("Playlists");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Description).HasMaxLength(500);
        builder.Property(p => p.CoverImagePath);
        builder.Property(p => p.IsDeleted).IsRequired();

        builder.HasData(LoadData());
    }

    private static List<Playlist> LoadData()
    {
        return new List<Playlist>
        {
            new Playlist
            {
                Id = 1,
                Name = "Chill Vibes",
                Description = "Relaxing songs for chill time",
                CoverImagePath = "/CoverImages/playlist1.jpg",
                IsDeleted = false
            },
            new Playlist
            {
                Id = 2,
                Name = "Workout Mix",
                Description = "High energy songs for workout",
                CoverImagePath = "/CoverImages/playlist2.jpg",
                IsDeleted = false
            },
            new Playlist
            {
                Id = 3,
                Name = "Road Trip",
                Description = "Best songs for driving",
                CoverImagePath = "/CoverImages/playlist3.jpg",
                IsDeleted = false
            }
        };
    }
}
