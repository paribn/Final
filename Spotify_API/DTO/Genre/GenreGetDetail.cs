using Spotify_API.DTO.Artist;
using Spotify_API.DTO.Music;

namespace Spotify_API.DTO.Genre
{
    public class GenreGetDetail
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public List<MusicGetDto> Musics { get; set; }

        public List<ArtistGetDto> Artists { get; set; }
    }
}
