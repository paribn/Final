using Spotify_API.Helpers.Enums;

namespace Spotify_API.DTO.Artist
{
    public class ArtistGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ArtistTypes ArtistType { get; set; }
        public List<ArtistPhotoGetDto> artistPhotos { get; set; }
    }
}
