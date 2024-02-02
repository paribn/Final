namespace Spotify_API.Entities
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<MusicPlayList> MusicPlayLists { get; set; }
    }
}
