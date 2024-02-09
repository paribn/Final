namespace Spotify_API.Entities
{
    public class MusicGenre
    {
        public int Id { get; set; }
        public int MusicId { get; set; }
        public int GenreId { get; set; }
        public virtual Music Music { get; set; }
        public virtual Genre Genre { get; set; }

    }
}
