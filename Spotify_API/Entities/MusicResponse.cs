using Spotify_API.DTO.Music;

namespace Spotify_API.Entities
{
    public class MusicResponse
    {
        public List<MusicGetDto> Musics { get; set; } = new List<MusicGetDto>();

        public int Pages { get; set; }
        public int CurrentPages { get; set; }
    }
}
