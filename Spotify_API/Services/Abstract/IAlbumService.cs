using Spotify_API.DTO.Album;

namespace Spotify_API.Services.Abstract
{
    public interface IAlbumService
    {
        Task CreateAsync(AlbumPostDto albumPostDto);
    }
}
