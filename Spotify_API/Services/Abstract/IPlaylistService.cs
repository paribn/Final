using Spotify_API.DTO.PlayList;

namespace Spotify_API.Services.Abstract
{
    public interface IPlaylistService
    {
        Task<List<PlaylistGetDto>> GetAsync();
        Task<PlaylistDetailDto> GetDetailAsync(int id);

        Task CreateAsync(PlayListPostDto playListPostDto);

        Task UpdateAsync(int id, PlaylistPutDto playlistPutDto);

        Task DeleteAsync(int id);


    }
}
