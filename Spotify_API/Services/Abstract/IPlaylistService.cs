using Spotify_API.DTO.PlayList;

namespace Spotify_API.Services.Abstract
{
    public interface IPlaylistService
    {
        Task<List<PlaylistGetDto>> GetAsync(int? page = null, int? perPage = null, string playListName = null);
        Task<PlaylistDetailDto> GetDetailAsync(int id);

        Task<List<PlaylistGetUser>> PlaylistGetUser(int? page = null, int? perPage = null, string playListName = null, string userId = null);
        Task CreateAsync(PlayListPostDto playListPostDto);

        Task UpdateAsync(int id, PlaylistPutDto playlistPutDto);

        Task DeleteAsync(int id);


    }
}
