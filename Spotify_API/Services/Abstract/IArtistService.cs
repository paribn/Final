using Spotify_API.DTO.Artist;

namespace Spotify_API.Services.Abstract
{
    public interface IArtistService
    {
        Task CreateAsync(ArtistPostDto artistPostDto);
        Task UpdateAsync(int id, ArtistPutDto artistPutDto);

        Task DeleteAsync(int id);


        Task<List<ArtistGetDto>> GetAllAsync();

        Task<ArtistGetDetail> GetDetailAsync(int id);
    }
}
