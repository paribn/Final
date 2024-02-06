namespace Spotify_API.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public byte[]? CoverImage { get; set; }

        // public string CoverImage { get; set; }

        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
        public List<Music> Musics { get; set; }
    }
}
