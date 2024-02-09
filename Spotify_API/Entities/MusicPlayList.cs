namespace Spotify_API.Entities
{
    public class MusicPlayList
    {
        public int Id { get; set; }
        public int PlayListId { get; set; }
        public int MusicId { get; set; }
        public Playlist Playlist { get; set; }
        public virtual Music Music { get; set; }
    }
}
