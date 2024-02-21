using Spotify_API.DTO.Album;
using Spotify_API.DTO.Music;

namespace Spotify_API.DTO.Artist
{
    public class ArtistGetDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public List<ArtistPhotoGetDto> artistPhotos { get; set; }

        public List<AlbumGetDtoForArtist> albumGets { get; set; }

        public List<MusicAlbumGetDto> MusicGet { get; set; }









        //public ArtistTypes ArtistType { get; set; }

        //public List<AlbumGetDto> Albums { get; set; }


        //public List<MusicGetDto> MusicGetDtos { get; set; }


    }
}
