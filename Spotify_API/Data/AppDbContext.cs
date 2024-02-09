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
        public DbSet<Music> Musics { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MusicGenre> MusicGenres { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<MusicPlayList> MusicPlayLists { get; set; }
        public DbSet<Album> Albums { get; set; }


    }
}
