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
        builder.Property(f => f.CreatedAt).IsRequired();

        builder.HasData(new Favourite
        {
            Id = 1,
            CreatedAt = new DateTime(2026, 1, 1)
        });
    }
}
