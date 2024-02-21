using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spotify_API.Data;
using Spotify_API.DTO.Artist;
using Spotify_API.Entities;
using Spotify_API.Services.Abstract;

namespace Spotify_API.Services.Concrete
{
    public class ArtistService : IArtistService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public ArtistService(AppDbContext appDbContext, IMapper mapper, IFileService fileService)
        {
            _context = appDbContext;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task CreateAsync(ArtistPostDto artistPostDto)
        {
            if (artistPostDto == null)
            {
                throw new ArgumentNullException(nameof(artistPostDto), "Artist DTO cannot be null");
            }

            var existingArtist = await _context.Artists.FirstOrDefaultAsync(a => a.Name == artistPostDto.Name);
            if (existingArtist != null)
            {
                throw new ArgumentException("Artist with the same name already exists", nameof(artistPostDto.Name));
            }

            var artist = _mapper.Map<Artist>(artistPostDto);

            if (artist == null)
            {
                throw new InvalidOperationException("Mapping failed, artist could not be created");
            }

            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var albums = _context.Artists.FirstOrDefault(x => x.Id == id);
            if (albums is null)
                throw new ArgumentNullException(nameof(albums), "Music not found");

            _context.Remove(albums);
            await _context.SaveChangesAsync();
        }


        public async Task<List<ArtistGetDto>> GetAllAsync()
        {
            var artist = await _context.Artists
             .Include(x => x.ArtistPhoto)
             .ToListAsync();

            if (artist is null) throw new Exception("Something went wrong!");

            var artistDtos = _mapper.Map<List<ArtistGetDto>>(artist);

            return artistDtos;
        }

        public async Task<ArtistGetDetail> GetDetailAsync(int id)
        {
            var artist = await _context.Artists
              .Include(x => x.ArtistPhoto)
             .Include(x => x.Musics)
             .Include(x => x.Albums)
            .FirstOrDefaultAsync(x => x.Id == id);
            if (artist == null) return null;

            var artistDetail = _mapper.Map<ArtistGetDetail>(artist);

            return artistDetail;
        }

        public async Task UpdateAsync(int id, ArtistPutDto artistPutDto)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(x => x.Id == id);
            if (artist == null)
            {
                throw new Exception("Artist not found");
            }

            _mapper.Map(artistPutDto, artist);

            await _context.SaveChangesAsync();

        }

        public async Task AddPhoto(ArtistPhotoCreateDto photoDto)
        {
            var existingArtist = await _context.Artists.FirstOrDefaultAsync(a => a.Id == photoDto.ArtistId);
            if (existingArtist == null)
            {
                throw new ArgumentException("Artist with the specified ID does not exist.");
            }

            foreach (var photo in photoDto.PhotoPath)
            {
                var artistPhoto = new ArtistPhoto();
                _mapper.Map(photoDto, artistPhoto);

                if (photo != null)
                {
                    var photoPath = _fileService.UploadFile(photo);
                    artistPhoto.PhotoPath = photoPath;
                }

                _context.ArtistPhotos.Add(artistPhoto);
            }

            await _context.SaveChangesAsync();
        }


        public async Task UpdatePhoto(int id, ArtistPhotoUpdateDto photoDto)
        {

            var photo = await _context.ArtistPhotos.FirstOrDefaultAsync(x => x.Id == id);
            if (photo == null) { throw new Exception("Something went wrong"); }
            if (photoDto.PhotoPath != null)
            {
                string newPhoto = _fileService.UploadFile(photoDto.PhotoPath);
                if (!string.IsNullOrEmpty(photo.PhotoPath))
                {
                    _fileService.DeleteFile(photo.PhotoPath);

                }
                photo.PhotoPath = newPhoto;
            }
            await _context.SaveChangesAsync();


        }


    }
}
