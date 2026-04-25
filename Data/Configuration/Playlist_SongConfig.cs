using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musicana.Api.Models;

namespace Musicana.Api.Data.Configuration;

public class Playlist_SongConfig : IEntityTypeConfiguration<Playlist_Song>
{
    public void Configure(EntityTypeBuilder<Playlist_Song> builder)
    {
        builder.HasKey(ps => new { ps.PlaylistId, ps.SongId });
        builder.ToTable("Playlist_Songs");

        builder.Property(ps => ps.Order).IsRequired();

        builder.HasOne(ps => ps.Playlist)
            .WithMany(p => p.playlist_Songs)
            .HasForeignKey(ps => ps.PlaylistId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ps => ps.Song)
            .WithMany(s => s.playlist_Songs)
            .HasForeignKey(ps => ps.SongId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(LoadData());
    }

    private static List<Playlist_Song> LoadData()
    {
        return new List<Playlist_Song>
        {
            new Playlist_Song { PlaylistId = 1, SongId = 1, Order = 1 },
            new Playlist_Song { PlaylistId = 1, SongId = 3, Order = 2 },
            new Playlist_Song { PlaylistId = 1, SongId = 5, Order = 3 },
            new Playlist_Song { PlaylistId = 2, SongId = 2, Order = 1 },
            new Playlist_Song { PlaylistId = 2, SongId = 4, Order = 2 },
            new Playlist_Song { PlaylistId = 2, SongId = 6, Order = 3 },
            new Playlist_Song { PlaylistId = 3, SongId = 1, Order = 1 },
            new Playlist_Song { PlaylistId = 3, SongId = 7, Order = 2 },
            new Playlist_Song { PlaylistId = 3, SongId = 8, Order = 3 }
        };
    }
}
