using Spotify_API.Helpers.Enums;

namespace Spotify_API.DTO.Artist
{
    public class ArtistPostDto
    {
        public string Name { get; set; }
        public string About { get; set; }

        public ArtistTypes ArtistTypes { get; set; }

        //public List<AlbumPostDto>? AlbumPostDtos { get; set; }
        //public List<GenrePostDto>? GenrePostDtos { get; set; }

        //public List<MusicPostDto> MusicPostDtos { get; set; }


    }
}
