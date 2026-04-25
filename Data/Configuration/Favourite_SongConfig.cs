using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musicana.Api.Models;

namespace Musicana.Api.Data.Configuration;

public class Favourite_SongConfig : IEntityTypeConfiguration<Favourite_Song>
{
    public void Configure(EntityTypeBuilder<Favourite_Song> builder)
    {
        builder.HasKey(fs => new { fs.FavouriteId, fs.SongId });
        builder.ToTable("Favourite_Songs");

        builder.Property(fs => fs.AddedAt).IsRequired();

        builder.HasOne(fs => fs.Favourite)
            .WithMany(f => f.favourite_Songs)
            .HasForeignKey(fs => fs.FavouriteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(fs => fs.Song)
            .WithMany(s => s.favourite_Songs)
            .HasForeignKey(fs => fs.SongId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
