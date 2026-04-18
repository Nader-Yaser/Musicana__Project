using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musicana.Api.Models;

namespace Musicana.Api.Data.Configuration;

public class FavouriteConfig : IEntityTypeConfiguration<Favourite>
{
    public void Configure(EntityTypeBuilder<Favourite> builder)
    {
        builder.ToTable("Favourites");
        builder.HasKey(f => f.Id);

        builder.Property(f => f.AddedAt).IsRequired();

        // Unique — كل أغنية ممكن تتضاف مرة واحدة بس
        builder.HasIndex(f => f.SongId).IsUnique();

        // العلاقة: كل Favourite مرتبط بأغنية واحدة
        builder.HasOne(f => f.Song)
            .WithOne(s => s.Favourite)
            .HasForeignKey<Favourite>(f => f.SongId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
