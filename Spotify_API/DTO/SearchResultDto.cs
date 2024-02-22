using Spotify_API.DTO.Genre;
using Spotify_API.DTO.Music;

namespace Spotify_API.DTO
{
    public class SearchResultDto
    {
        public List<MusicGetDto> musicGets { get; set; }
        public List<GenreGetDto> Genres { get; set; }
    }
}




