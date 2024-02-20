using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            if (genre is null) throw new Exception("Something went wrong!");
            _context.Remove(genre);
            _context.SaveChanges();
        }

        public async Task<List<GenreGetDto>> GetAllAsync()
        {

            var genres = await _context.Genres
              .ToListAsync();

            var genreDtos = _mapper.Map<List<GenreGetDto>>(genres);

            return genreDtos;
        }

        public async Task<GenreGetDetail> GetDetailAsync(int id)
        {
            var genre = await _context.Genres
                .Include(g => g.MusicGenres)
                 .ThenInclude(x => x.Music)
                .ThenInclude(d => d.Artist)
               .FirstOrDefaultAsync(g => g.Id == id);

            var genreDetailDto = _mapper.Map<GenreGetDetail>(genre);

            return genreDetailDto;
        }

        public async Task UpdateAsync(int id, GenrePutDto genrePutDto)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (genre is null) throw new ArgumentNullException(nameof(genre), "Genre not found");

            _mapper.Map(genrePutDto, genre);

            await _context.SaveChangesAsync();

        }


    }
}
