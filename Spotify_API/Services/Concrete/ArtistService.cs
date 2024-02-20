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
            var artist = _mapper.Map<Artist>(artistPostDto);

            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var artist = await _context.Artists
             .Include(a => a.Albums)
             .Include(a => a.Musics)
             .FirstOrDefaultAsync(a => a.Id == id);

            if (artist == null) throw new ArgumentNullException();


            _context.Albums.RemoveRange(artist.Albums);
            _context.Musics.RemoveRange(artist.Musics);

            _context.Artists.Remove(artist);

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
                throw new Exception("artist tapilmadi");
            }

            _mapper.Map(artistPutDto, artist);

            await _context.SaveChangesAsync();



        }

        public async Task AddPhoto(int artistId, ArtistPhotoCreateDto photoDto)
        {
            var artist = await _context.Artists.Include(a => a.ArtistPhoto).FirstOrDefaultAsync(a => a.Id == artistId);
            if (artist == null) throw new Exception();


            var photoArtist = new ArtistPhoto();
            _mapper.Map(photoDto, photoArtist);

            var photo = photoDto.PhotoPath;
            if (photo != null)
            {
                var photoPath = _fileService.UploadFile(photo);
                photoArtist.PhotoPath = photoPath;
            };


            artist.ArtistPhoto.Add(photoArtist);
            await _context.SaveChangesAsync();





            //var album = new Album();
            //_mapper.Map(albumPostDto, album);


            //var coverImageFile = albumPostDto.CoverImage;
            //if (coverImageFile != null)
            //{
            //    var coverImageUrl = _fileService.UploadFile(coverImageFile);

            //    album.CoverImage = coverImageUrl;
            //}

            //_context.Albums.Add(album);
            //await _context.SaveChangesAsync();
        }


    }
}
