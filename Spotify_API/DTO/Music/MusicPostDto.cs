namespace Spotify_API.DTO.Music
{
    public class MusicPostDto
    {
        public string Title { get; set; }
        public int Duration { get; set; }
        public int ListenCount { get; set; }

        public string MusicUrl { get; set; }

        public string PhotoUrl { get; set; }


        public int AlbumId { get; set; }
        public int ArtistId { get; set; }

        public List<int>? PlayListId { get; set; }

        public List<int>? GenreId { get; set; }
        //public List<GenrePostDto> GenrePostDtos { get; set; }


    }
}
