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

            if (artistPostDto.PhotoPath == null || !artistPostDto.PhotoPath.Any())
            {
                throw new ArgumentException("At least one photo must be provided", nameof(artistPostDto.PhotoPath));
            }

            var artist = _mapper.Map<Artist>(artistPostDto);

            if (artist == null)
            {
                throw new InvalidOperationException("Mapping failed, artist could not be created");
            }

            List<ArtistPhoto> artistPhotos = new List<ArtistPhoto>();

            foreach (var file in artistPostDto.PhotoPath)
            {
                artistPhotos.Add(new ArtistPhoto
                {
                    PhotoPath = _fileService.UploadFile(file)
                });
            }

            artistPhotos.FirstOrDefault().IsMain = true;
            artist.ArtistPhoto = artistPhotos;

            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();
        }



        public async Task DeleteAsync(int id)
        {

            var artist = _context.Artists.Include(x => x.ArtistPhoto).FirstOrDefault(x => x.Id == id);
            if (artist is null)
                throw new ArgumentNullException(nameof(artist), "Artist not found");
            if (artist.ArtistPhoto != null)
            {
                foreach (var file in artist.ArtistPhoto)
                {
                    _fileService.DeleteFile(file.PhotoPath);
                }
            }
            _context.Artists.Remove(artist);
            _context.SaveChanges();


            //var albums = await _context.Albums.FirstOrDefaultAsync(x => x.Id == id);
            //if (albums is null) throw new Exception("Something went wrong!");
            //_fileService.DeleteFile(albums.CoverImage);

            //_context.Albums.Remove(albums);
            //await _context.SaveChangesAsync();
        }


        public async Task<List<ArtistGetDto>> GetAllAsync(int? page = null, int? perPage = null, string artistName = null)
        {
            IQueryable<Artist> query = _context.Artists.Include(x => x.ArtistPhoto);

            if (!string.IsNullOrEmpty(artistName))
            {
                query = query.Where(a => a.Name.Contains(artistName));
            }

            if (page.HasValue && perPage.HasValue)
            {
                int totalCount = await query.CountAsync();
                int totalPages = (int)Math.Ceiling((double)totalCount / perPage.Value);
                page = Math.Min(Math.Max(page.Value, 1), totalPages); // Page number validation

                int skip = (page.Value - 1) * perPage.Value;

                query = query.Skip(skip).Take(perPage.Value);
            }

            var artists = await query.ToListAsync();
            var artistDtos = _mapper.Map<List<ArtistGetDto>>(artists);

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
            var artist = await _context.Artists.Include(x => x.ArtistPhoto).FirstOrDefaultAsync(x => x.Id == id);
            if (artist == null)
            {
                throw new Exception("Artist not found");
            }

            _mapper.Map(artistPutDto, artist);

            if (artistPutDto.PhotoPath != null)
            {
                if (artist.ArtistPhoto != null)
                {
                    foreach (var file in artist.ArtistPhoto)
                    {
                        _fileService.DeleteFile(file.PhotoPath);
                    }
                }
            }

            List<ArtistPhoto> photos = new List<ArtistPhoto>();

            foreach (var file in artistPutDto.PhotoPath)
            {
                photos.Add(new()
                {
                    PhotoPath = _fileService.UploadFile(file)
                });
            }

            photos.FirstOrDefault().IsMain = true;
            artist.ArtistPhoto = photos;

            _context.Artists.Update(artist);

            await _context.SaveChangesAsync();

        }










        //public async Task AddPhoto(ArtistPhotoCreateDto photoDto)
        //{
        //    var existingArtist = await _context.Artists.FirstOrDefaultAsync(a => a.Id == photoDto.ArtistId);
        //    if (existingArtist == null)
        //    {
        //        throw new ArgumentException("Artist with the specified ID does not exist.");
        //    }

        //    foreach (var photo in photoDto.PhotoPath)
        //    {
        //        var artistPhoto = new ArtistPhoto();
        //        _mapper.Map(photoDto, artistPhoto);

        //        if (photo != null)
        //        {
        //            var photoPath = _fileService.UploadFile(photo);
        //            artistPhoto.PhotoPath = photoPath;
        //        }

        //        _context.ArtistPhotos.Add(artistPhoto);
        //    }

        //    await _context.SaveChangesAsync();
        //}


        //public async Task UpdatePhoto(ArtistPhotoUpdateDto photoDto, int id)
        //{
        //    var existingPhotos = await _context.ArtistPhotos.FirstOrDefaultAsync(ap => ap.Id == id);

        //    // Yeni fotoğrafları ekle
        //    if (photoDto.PhotoPath != null)
        //    {
        //        if (existingPhotos.PhotoPath != null)
        //        {
        //            _fileService.DeleteFile(existingPhotos.PhotoPath);
        //        }

        //        existingPhotos.ProductImage.ImageName = _fileService.UploadFile(model.Photo);
        //    }

        //    // Eski fotoğrafları sil
        //    foreach (var existingPhoto in existingPhotos.PhotoPath)
        //    {
        //        _context.ArtistPhotos.Remove(existingPhoto);
        //        _fileService.DeleteFile(existingPhoto.); // Önceki fotoğrafı sil
        //    }

        //    await _context.SaveChangesAsync();
        //}




    }
}
