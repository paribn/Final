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
                throw new Exception("You must select at least one genre.");
            }

        }

        public async Task<List<MusicGetDto>> GetAsync()
        {
            var musicGetDtos = await _context.Musics
                .Include(x => x.Artist)
                .Include(x => x.Album)
                .Select(x => _mapper.Map(x, new MusicGetDto()))// Her müzik için MusicGetDto oluştur
                .AsNoTracking()
                .ToListAsync(); // Senkron bir şekilde koleksiyonu döndür

            return musicGetDtos;
        }


    }
}
