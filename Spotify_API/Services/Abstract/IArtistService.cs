using Spotify_API.DTO.Artist;

namespace Spotify_API.Services.Abstract
{
    public interface IArtistService
    {
        Task CreateAsync(ArtistPostDto artistPostDto);
        Task UpdateAsync(int id, ArtistPutDto artistPutDto);

        Task DeleteAsync(int id);


        Task<List<ArtistGetDto>> GetAllAsync(int? page = null, int? perPage = null, string artistName = null);

        Task<ArtistGetDetail> GetDetailAsync(int id);




        /// artist phhoto add & update

        //Task AddPhoto(ArtistPhotoCreateDto photoDto);

        //Task UpdatePhoto(ArtistPhotoUpdateDto photoDto, int id);

    }
}
