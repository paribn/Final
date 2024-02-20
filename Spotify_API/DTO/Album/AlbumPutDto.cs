namespace Spotify_API.DTO.Album
{
    public class AlbumPutDto
    {
        public string Title { get; set; }

        public IFormFile? CoverImage { get; set; }
    }
}
