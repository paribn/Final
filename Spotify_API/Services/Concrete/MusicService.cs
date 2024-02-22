using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spotify_API.Data;
using Spotify_API.DTO.Music;
using Spotify_API.Entities;
using Spotify_API.Services.Abstract;

namespace Spotify_API.Services.Concrete
{
    public class MusicService : IMusicService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public MusicService(AppDbContext appDbContext, IMapper mapper, IFileService fileService)
        {
            _context = appDbContext;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task CreateAsync(MusicPostDto musicPostDto)
        {
            var artistExists = await _context.Artists.AnyAsync(a => a.Id == musicPostDto.ArtistId);
            if (!artistExists)
            {
                throw new ArgumentException("Invalid type ID.");
            }

            var albumExists = await _context.Albums.AnyAsync(a => a.Id == musicPostDto.AlbumId);
            if (!albumExists)
            {
                throw new ArgumentException("Invalid type ID.");
            }

            var genres = await _context.Genres.Where(g => musicPostDto.GenreId.Contains(g.Id)).ToListAsync();
            if (genres == null || !genres.Any())
            {
                throw new ArgumentException("Invalid type ID.");
            }

            var music = _mapper.Map<Music>(musicPostDto);
            music.MusicGenres = genres.Select(genre => new MusicGenre { GenreId = genre.Id }).ToList();

            var coverImageFile = musicPostDto.PhotoUrl;
            var mp3 = musicPostDto.MusicUrl;

            if (coverImageFile != null && mp3 != null)
            {
                var coverImageUrl = _fileService.UploadFile(coverImageFile);
                var musicUrl = _fileService.UploadMusic(mp3);

                music.PhotoUrl = coverImageUrl;
                music.MusicUrl = musicUrl;
            }

            _context.Musics.Add(music);
            await _context.SaveChangesAsync();
        }



        public async Task<List<MusicGetDto>>? GetAsync(int? page = null, int? perPage = null, string name = null)
        {
            IQueryable<Music> query = _context.Musics.Include(x => x.Artist)
                .Include(a => a.Album);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.Name.Contains(name));
            }
            if (page.HasValue && perPage.HasValue)
            {
                int totalCount = await query.CountAsync();
                int totalPages = (int)Math.Ceiling((double)totalCount / perPage.Value);
                page = Math.Min(Math.Max(page.Value, 1), totalPages);

                int skip = (page.Value - 1) * perPage.Value;

                query = query.Skip(skip).Take(perPage.Value);
            }

            var musics = await query.ToListAsync();
            var musicDtos = _mapper.Map<List<MusicGetDto>>(musics);

            return musicDtos;

        }

        public async Task<MusicGetDetail> GetDetailAsync(int id)
        {
            var music = await _context.Musics
             .Include(x => x.Artist)
             .Include(x => x.Album)
            .FirstOrDefaultAsync(x => x.Id == id);
            if (music == null) return null;

            var musicDetail = _mapper.Map<MusicGetDetail>(music);

            return musicDetail;
        }

        public async Task UpdateAsync(int id, MusicPutDto musicPutDto)
        {
            var music = await _context.Musics.FirstOrDefaultAsync(x => x.Id == id);
            if (music == null)
            {
                throw new ArgumentNullException(nameof(music), "Music not found");
            }

            _mapper.Map(musicPutDto, music);


            if (music.PhotoUrl != null)
            {
                _fileService.DeleteFile(music.PhotoUrl);  /// 
            }

            if (musicPutDto.PhotoUrl != null)
            {
                music.PhotoUrl = _fileService.UploadFile(musicPutDto.PhotoUrl);
            }



            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var music = await _context.Musics.FirstOrDefaultAsync(x => x.Id == id);
            if (music is null)
                throw new ArgumentNullException(nameof(music), "Music not found");

            _fileService.DeleteMusic(music.MusicUrl);
            _fileService.DeleteFile(music.PhotoUrl);

            _context.Remove(music);
            await _context.SaveChangesAsync();

        }


    }
}




