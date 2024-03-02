using Spotify_API.DTO.Album;
using Spotify_API.DTO.Artist;
using Spotify_API.DTO.Music;
using Spotify_API.Helpers.Enums;

namespace Spotify_API.DTO
{
    public class SearchResultDto
    {
        //ALL SEARCH METHOD
        public int Id { get; set; }
        public string Name { get; set; }

        public ArtistTypes ArtistType { get; set; }

        public List<ArtistPhotoGetDto> artistPhotos { get; set; }
        public List<MusicGetDto> Music { get; set; }
        public List<AlbumGetDto> Album { get; set; }
    }
}




