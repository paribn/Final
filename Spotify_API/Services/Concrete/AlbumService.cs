using AutoMapper;
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
    }
}
