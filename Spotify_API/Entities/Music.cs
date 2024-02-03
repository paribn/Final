namespace Spotify_API.Entities
{
    public class Music
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public int ListenCount { get; set; }

        public int? ArtistId { get; set; }
        public Artist Artist { get; set; }
        //public int? GroupId { get; set; }
        //public virtual Group Group { get; set; }
        public int? AlbumId { get; set; }
        public virtual Album Album { get; set; }

        public ICollection<MusicGenre> Genres { get; set; }

        public List<MusicPlayList>? MusicPlayLists { get; set; }

    }
}
