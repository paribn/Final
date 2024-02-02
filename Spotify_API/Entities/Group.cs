namespace Spotify_API.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public List<Music> Musics { get; set; }
        public List<Artist> Artists { get; set; }
        public List<Album> Albums { get; set; }
    }
}
