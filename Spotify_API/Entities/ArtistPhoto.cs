namespace Spotify_API.Entities
{
    public class ArtistPhoto
    {
        public int Id { get; set; }
        public string PhotoPath { get; set; }

        public bool IsMain { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}
