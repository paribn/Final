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
        private readonly IFileService _fileService;
        public GenreService(AppDbContext appDbContext, IMapper mapper, IFileService fileService)
        {
            _context = appDbContext;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task CreateAsync(GenrePostDto genrePostDto)
        {
            var existingGenre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == genrePostDto.Name);
            if (existingGenre != null)
            {
                throw new ArgumentException("A genre with the same name already exists.");
            }

            var genre = new Genre();
            _mapper.Map(genrePostDto, genre);

            var image = genrePostDto.PhotoPath;
            if (image != null)
            {
                var imageUrl = _fileService.UploadFile(image);
                genre.PhotoPath = imageUrl;
            }
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

        }


        public async Task<List<GenreGetDto>> GetAllAsync(int? page = null, int? perPage = null, string genreName = null)
        {
            IQueryable<Genre> query = _context.Genres;

            if (!string.IsNullOrEmpty(genreName))
            {
                query = query.Where(a => a.Name.Contains(genreName));
            }
            if (page.HasValue && perPage.HasValue)
            {
                int totalCount = await query.CountAsync();
                int totalPages = (int)Math.Ceiling((double)totalCount / perPage.Value);
                page = Math.Min(Math.Max(page.Value, 1), totalPages); // Page number validation

                int skip = (page.Value - 1) * perPage.Value;

                query = query.Skip(skip).Take(perPage.Value);
            }

            var genres = await query.ToListAsync();
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

            genre.Name = genrePutDto.Name;
            if (genrePutDto.photoPath != null)
            {
                string newPhoto = _fileService.UploadFile(genrePutDto.photoPath);
                if (!string.IsNullOrEmpty(genre.PhotoPath))
                {
                    _fileService.DeleteFile(genre.PhotoPath);
                }
                genre.PhotoPath = newPhoto;
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var genre = _context.Genres.FirstOrDefault(x => x.Id == id);
            if (genre is null) throw new Exception("Something went wrong!");
            _context.Remove(genre);
            _context.SaveChanges();
        }



    }
}
