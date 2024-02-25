using Microsoft.AspNetCore.Identity;

namespace Spotify_API.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        //public int Age { get; set; }
        //public Gender Gender { get; set; }
        public List<Playlist> Playlists { get; set; }
    }
}
