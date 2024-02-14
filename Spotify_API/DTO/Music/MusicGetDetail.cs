namespace Spotify_API.DTO.Music
{
    public class MusicGetDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }

        public string MusicUrl { get; set; }
        public string PhotoUrl { get; set; }

        public string? Artistname { get; set; }

        public string? AlbumName { get; set; }

        public string? ArtistType { get; set; }
    }
}
