using Spotify_API.DTO.Artist;

namespace Spotify_API.Services.Abstract
{
    public interface IArtistService
    {
        Task CreateAsync(ArtistPostDto artistPostDto);
        Task UpdateAsync(int id, ArtistPutDto artistPutDto);
    }
}
