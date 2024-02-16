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
            var genre = await _context.Genres.FirstOrDefaultAsync(x => musicPostDto.GenreId.Contains(x.Id));
            var artistExists = await _context.Artists.AnyAsync(a => a.Id == musicPostDto.ArtistId);

            if (!artistExists)
            {
                throw new ArgumentException("Invalid artist ID.");
            }

            var genreExists = await _context.Albums.AnyAsync(g => g.Id == musicPostDto.AlbumId);

            if (!genreExists)
            {
                throw new ArgumentException("Invalid album ID.");
            }

            if (genre != null)
            {
                var music = _mapper.Map<Music>(musicPostDto);
                var coverImageFile = musicPostDto.PhotoUrl;
                var mp3 = musicPostDto.MusicUrl;

                if (coverImageFile != null && mp3 != null)
                {
                    var coverImageUrl = _fileService.UploadFile(coverImageFile);
                    var musicUrl = _fileService.UploadMusic(mp3);

                    music.PhotoUrl = coverImageUrl;
                    music.MusicUrl = musicUrl;
                }
                music.MusicGenres = new List<MusicGenre>();

                var musicgenre = new MusicGenre
                {
                    MusicId = music.Id,
                    GenreId = genre.Id,
                };

                music.MusicGenres.Add(musicgenre);

                _context.Musics.Add(music);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception();
            }

        }



        public async Task<List<MusicGetDto>>? GetAsync(int page = 1, int pageSize = 5)
        {
            var totalCount = _context.Musics.Count();

            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var musicPerPage = await _context.Musics
                .Include(x => x.Artist)
                .Include(x => x.Album)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => _mapper.Map(x, new MusicGetDto()))
                .AsNoTracking()
                .ToListAsync(); // disabled


            //var musicGetDtos = await _context.Musics
            //    .Include(x => x.Artist)
            //    .Include(x => x.Album)
            //    .Select(x => _mapper.Map(x, new MusicGetDto()))
            //    .AsNoTracking()
            //    .ToListAsync();

            return musicPerPage;
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




