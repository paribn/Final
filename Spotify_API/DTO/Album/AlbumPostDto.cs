
namespace Spotify_API.DTO.Album
{
    public class AlbumPostDto
    {
        public string Title { get; set; }
        public IFormFile? CoverImage { get; set; }

        public int ArtisId { get; set; }
    }
}
