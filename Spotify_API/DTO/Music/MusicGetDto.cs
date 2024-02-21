namespace Spotify_API.DTO.Music
{
    public class MusicGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public int Duration { get; set; }

        public string MusicUrl { get; set; }
        public string PhotoUrl { get; set; }

        public string? Artistname { get; set; }


    }
}
