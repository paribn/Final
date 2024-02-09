using Spotify_API.DTO.PlayList;

namespace Spotify_API.Services.Abstract
{
    public interface IPlaylistService
    {
        Task CreateAsync(PlayListPostDto playListPostDto);
    }
}
