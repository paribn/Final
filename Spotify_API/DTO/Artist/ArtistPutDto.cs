using Spotify_API.Helpers.Enums;

namespace Spotify_API.DTO.Artist
{
    public class ArtistPutDto
    {
        public string Name { get; set; }
        public string About { get; set; }
        public ArtistTypes ArtistType { get; set; }

    }
}
