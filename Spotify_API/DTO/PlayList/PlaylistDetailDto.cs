using Spotify_API.DTO.Music;

namespace Spotify_API.DTO.PlayList
{
    public class PlaylistDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<MusicGetDto> musicGets { get; set; }
    }
}
