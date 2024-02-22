using Spotify_API.DTO.Album;
using Spotify_API.DTO.Genre;

namespace Spotify_API.DTO.Music
{
    public class MusicGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string MusicUrl { get; set; }
        public string PhotoUrl { get; set; }

        public string? Artistname { get; set; }


        //ALL SEARCH METHOD
        public AlbumGetDto Album { get; set; }
        public GenreGetDto Genre { get; set; }
    }
}
