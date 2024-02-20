using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spotify_API.Data;
using Spotify_API.DTO.Album;
using Spotify_API.Entities;
using Spotify_API.Services.Abstract;

namespace Spotify_API.Services.Concrete
{
    public class AlbumService : IAlbumService
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public AlbumService(AppDbContext appDbContext, IMapper mapper, IFileService fileService)
        {
            _context = appDbContext;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task CreateAsync(AlbumPostDto albumPostDto)
        {
            var album = new Album();
            _mapper.Map(albumPostDto, album);


            var coverImageFile = albumPostDto.CoverImage;
            if (coverImageFile != null)
            {
                var coverImageUrl = _fileService.UploadFile(coverImageFile);

                album.CoverImage = coverImageUrl;
            }

            _context.Albums.Add(album);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {

            var albums = _context.Albums.FirstOrDefault(x => x.Id == id);
            if (albums is null) throw new Exception("Something went wrong!");
            _fileService.DeleteFile(albums.CoverImage);

            _context.Remove(albums);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AlbumGetDto>> GetAllAsync()
        {
            var album = await _context.Albums
                .Include(x => x.Artist)
              .ToListAsync();

            var albumDtos = _mapper.Map<List<AlbumGetDto>>(album);

            return albumDtos;
        }

        public async Task<AlbumGetDetail> GetDetailAsync(int id)
        {
            var album = await _context.Albums
           .Include(g => g.Artist)
           .Include(x => x.Musics)
           .FirstOrDefaultAsync(g => g.Id == id);

            if (album == null) throw new Exception("Something went wrong!");

            var albumDetailDto = _mapper.Map<AlbumGetDetail>(album);

            return albumDetailDto;
        }
        public async Task UpdateAsync(int id, AlbumPutDto albumPutDto)
        {
            var album = await _context.Albums.FirstOrDefaultAsync(x => x.Id == id);
            if (album == null)
            {
                throw new InvalidOperationException("Album not found.");
            }

            album.Title = albumPutDto.Title;

            if (albumPutDto.CoverImage != null)
            {
                string newPhoto = _fileService.UploadFile(albumPutDto.CoverImage);

                if (!string.IsNullOrEmpty(album.CoverImage))
                {
                    _fileService.DeleteFile(album.CoverImage);
                }

                album.CoverImage = newPhoto;
            }

            await _context.SaveChangesAsync();
        }



    }
}

