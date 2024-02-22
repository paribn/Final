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

            if (albumPostDto.ArtisId != null)
            {
                var existingArtist = await _context.Artists.FirstOrDefaultAsync(a => a.Id == albumPostDto.ArtisId);
                if (existingArtist == null)
                {
                    throw new ArgumentException("Artist with the specified ID does not exist.");
                }
                album.ArtistId = albumPostDto.ArtisId;
            }

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

            var albums = await _context.Albums.FirstOrDefaultAsync(x => x.Id == id);
            if (albums is null) throw new Exception("Something went wrong!");
            _fileService.DeleteFile(albums.CoverImage);

            _context.Albums.Remove(albums);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AlbumGetDto>> GetAllAsync(int? page = null, int? perPage = null, string albumName = null)
        {

            IQueryable<Album> query = _context.Albums;

            if (!string.IsNullOrEmpty(albumName))
            {
                query = query.Where(a => a.Title.Contains(albumName));
            }

            if (page.HasValue && perPage.HasValue)
            {
                int totalCount = await query.CountAsync();
                int totalPages = (int)Math.Ceiling((double)totalCount / perPage.Value);
                page = Math.Min(Math.Max(page.Value, 1), totalPages);

                int skip = (page.Value - 1) * perPage.Value;

                query = query.Skip(skip).Take(perPage.Value);
            }

            var albums = await query.
                Include(x => x.Artist).
                ToListAsync();
            var albumDtos = _mapper.Map<List<AlbumGetDto>>(albums);

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
