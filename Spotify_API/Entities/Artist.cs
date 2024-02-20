using Spotify_API.Helpers.Enums;

namespace Spotify_API.Entities
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public ArtistTypes ArtistType { get; set; }

        public ICollection<ArtistPhoto> ArtistPhoto { get; set; }
        public virtual ICollection<Album>? Albums { get; set; }
        public virtual ICollection<Music>? Musics { get; set; }
    }
}
