using Spotify_API.DTO.Music;

namespace Spotify_API.DTO.Album
{
    public class AlbumGetDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CoverImage { get; set; }

        public string? Artistname { get; set; }

        public List<MusicAlbumGetDto> musicAlbumGetDtos { get; set; }

    }
}
