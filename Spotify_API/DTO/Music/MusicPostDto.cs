namespace Spotify_API.DTO.Music
{
    public class MusicPostDto
    {
        public string Title { get; set; }
        public int Duration { get; set; }
        //public int ListenCount { get; set; }
        public IFormFile? MusicUrl { get; set; }
        public IFormFile? PhotoUrl { get; set; }

        public int AlbumId { get; set; }
        public int ArtistId { get; set; }

        public List<int>? GenreId { get; set; }


    }
}
