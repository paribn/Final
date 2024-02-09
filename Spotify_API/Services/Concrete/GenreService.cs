using AutoMapper;
using Spotify_API.Data;
using Spotify_API.DTO.Genre;
using Spotify_API.Entities;
using Spotify_API.Services.Abstract;

namespace Spotify_API.Services.Concrete
{
    public class GenreService : IGenreService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GenreService(AppDbContext appDbContext, IMapper mapper)
        {
            _context = appDbContext;
            _mapper = mapper;
        }

        public async Task CreateAsync(GenrePostDto genrePostDto)
        {
            var genre = new Genre();
            _mapper.Map(genrePostDto, genre);

            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var genre = _context.Genres.FirstOrDefault(x => x.Id == id);

            _context.Remove(genre);
            _context.SaveChanges();
        }
    }
}
