using Microsoft.EntityFrameworkCore;
using Musicana.Api.Models;

namespace Musicana.Api.Data;

public class MusicanaDbContext : DbContext
{
    public MusicanaDbContext(DbContextOptions<MusicanaDbContext> options) : base(options)
    {
    }
    public DbSet<Musician> Musicians { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<Instrument> Instruments { get; set; }
    public DbSet<Musician_Song> Musician_Songs { get; set; }
    public DbSet<Musician_Instrument> Musician_Instruments { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<Playlist_Song> Playlist_Songs { get; set; }
    public DbSet<Favourite> Favourites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MusicanaDbContext).Assembly);
        modelBuilder.Entity<Song>().HasQueryFilter(s => s.IsDeleted == false);
        modelBuilder.Entity<Musician>().HasQueryFilter(s => s.IsDeleted == false);
        modelBuilder.Entity<Instrument>().HasQueryFilter(s => s.IsDeleted == false);
        modelBuilder.Entity<Album>().HasQueryFilter(a => a.IsDeleted == false);
        modelBuilder.Entity<Playlist>().HasQueryFilter(p => p.IsDeleted == false);
    }
}