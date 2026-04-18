using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musicana.Api.Models;

namespace Musicana.Api.Data.Configuration;

public class AlbumConfig : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.ToTable("Albums");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Title).HasMaxLength(100).IsRequired();
        builder.Property(a => a.Description).HasMaxLength(500);
        builder.Property(a => a.ReleaseDate).IsRequired();
        builder.Property(a => a.CoverImagePath);
        builder.Property(a => a.IsDeleted).IsRequired();

        builder.HasOne(a => a.Musician)
            .WithMany(m => m.Albums)
            .HasForeignKey(a => a.MusicianId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Songs)
            .WithOne(s => s.Album)
            .HasForeignKey(s => s.AlbumId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasData(LoadData());
    }

    private static List<Album> LoadData()
    {
        return new List<Album>
        {
            new Album
            {
                Id = 1,
                Title = "Album 1",
                Description = "First Album",
                ReleaseDate = new DateTime(2020, 1, 15),
                CoverImagePath = "/CoverImages/album1.jpg",
                IsDeleted = false,
                MusicianId = 1
            },
            new Album
            {
                Id = 2,
                Title = "Album 2",
                Description = "Second Album",
                ReleaseDate = new DateTime(2021, 6, 20),
                CoverImagePath = "/CoverImages/album2.jpg",
                IsDeleted = false,
                MusicianId = 2
            },
            new Album
            {
                Id = 3,
                Title = "Album 3",
                Description = "Third Album",
                ReleaseDate = new DateTime(2022, 3, 10),
                CoverImagePath = "/CoverImages/album3.jpg",
                IsDeleted = false,
                MusicianId = 1
            },
            new Album
            {
                Id = 4,
                Title = "Album 4",
                Description = "Fourth Album",
                ReleaseDate = new DateTime(2023, 8, 5),
                CoverImagePath = "/CoverImages/album4.jpg",
                IsDeleted = false,
                MusicianId = 3
            },
            new Album
            {
                Id = 5,
                Title = "Album 5",
                Description = "Fifth Album",
                ReleaseDate = new DateTime(2024, 2, 28),
                CoverImagePath = "/CoverImages/album5.jpg",
                IsDeleted = false,
                MusicianId = 4
            }
        };
    }
}
