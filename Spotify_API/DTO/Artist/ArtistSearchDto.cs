using Spotify_API.DTO.Album;
using Spotify_API.DTO.Music;

namespace Spotify_API.DTO.Artist
{
    public class ArtistSearchDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<MusicGetDto> Music { get; set; }
        public List<AlbumGetDto> AlbumsSearch { get; set; }
        //public string musicName { get; set; }
        //public string musicPhoto { get; set; }
        //public string albumName { get; set; }
        ////public string albumId { get; set; }
        //public string coverImage { get; set; }
        public List<ArtistPhotoGetDto> artistPhotos { get; set; }

    }
}
