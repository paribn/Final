using Spotify_API.DTO.Music;

namespace Spotify_API.Services.Abstract
{
    public interface IMusicService
    {
        Task CreateAsync(MusicPostDto musicPostDto);
        Task<List<MusicGetDto>> GetAsync(int page = 1, int pageSize = 10);

        Task<MusicGetDetail> GetDetailAsync(int id);

        Task DeleteAsync(int id);
        Task UpdateAsync(int id, MusicPutDto musicPutDto);



    }
}
