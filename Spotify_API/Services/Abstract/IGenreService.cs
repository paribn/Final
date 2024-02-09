using Spotify_API.DTO.Genre;

namespace Spotify_API.Services.Abstract
{
    public interface IGenreService
    {
        Task CreateAsync(GenrePostDto genrePostDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, GenrePutDto genrePutDto);

    }
}
