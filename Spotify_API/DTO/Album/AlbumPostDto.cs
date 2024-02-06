
namespace Spotify_API.DTO.Album
{
    public class AlbumPostDto
    {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IFormFile? CoverImage { get; set; }

        //public string CoverImage { get; set; }

        public int ArtisId { get; set; }
    }
}
