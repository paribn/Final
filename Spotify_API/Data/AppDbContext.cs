using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Spotify_API.Entities;

namespace Spotify_API.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MusicGenre>()
            .HasOne(bc => bc.Music)
            .WithMany(b => b.MusicGenres)
            .HasForeignKey(bc => bc.MusicId);

            modelBuilder.Entity<MusicGenre>()
                .HasOne(bc => bc.Genre)
                .WithMany(c => c.MusicGenres)
                .HasForeignKey(bc => bc.GenreId);

            modelBuilder.Entity<MusicGenre>()
                .HasKey(x => new { x.MusicId, x.GenreId });


            modelBuilder.Entity<MusicPlayList>()
          .HasOne(bc => bc.Music)
          .WithMany(b => b.MusicPlayLists)
          .HasForeignKey(bc => bc.MusicId);

            modelBuilder.Entity<MusicPlayList>()
                .HasOne(bc => bc.Playlist)
                .WithMany(c => c.MusicPlayLists)
                .HasForeignKey(bc => bc.PlayListId);

            modelBuilder.Entity<MusicPlayList>()
                .HasKey(x => new { x.MusicId, x.PlayListId });



        }
        public DbSet<Music> Musics { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MusicGenre> MusicGenres { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<MusicPlayList> MusicPlayLists { get; set; }
        public DbSet<Album> Albums { get; set; }



    }
}
