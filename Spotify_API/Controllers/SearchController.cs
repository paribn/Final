using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spotify_API.Data;
using Spotify_API.DTO;
using Spotify_API.DTO.Album;
using Spotify_API.DTO.Artist;
using Spotify_API.DTO.Music;
using Spotify_API.Entities;

namespace Spotify_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SearchController(AppDbContext appDbContext, IMapper mapper)
        {
            _context = appDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> SearchAsync(string input, int? page = null, int? perPage = null)
        {
            try
            {
                IQueryable<Artist> query = _context.Artists
                    .Include(x => x.Musics)
                    .Include(x => x.Albums);

                if (!string.IsNullOrEmpty(input))
                {
                    query = query.Where(a => a.Name.Contains(input) ||
                                             a.Musics.Any(m => m.Name == input));
                }

                var music = await query.Select(x => new SearchResultDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    artistPhotos = x.ArtistPhoto.Select(ap => new ArtistPhotoGetDto
                    {
                        Id = ap.Id,
                        PhotoPath = ap.PhotoPath,
                    }).ToList(),
                    Music = x.Musics.Select(m => new MusicGetDto
                    {
                        Id = m.Id,
                        Name = m.Name,
                        PhotoUrl = m.PhotoUrl
                    }).ToList(),
                    Album = x.Albums.Select(al => new AlbumGetDto
                    {
                        Id = al.Id,
                        Title = al.Title,
                        CoverImage = al.CoverImage
                    }).ToList(),

                }).ToListAsync();

                return Ok(music);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



    }
}
//[HttpGet]
//public async Task<ActionResult> SearchAsync(string input = null, int? page = null, int? perPage = null)
//{
//    IQueryable<Music> query = _context.Musics
//        .Include(x => x.Album)
//        .Include(x => x.Artist)
//        .Include(x => x.MusicGenres)
//        .ThenInclude(x => x.Genre);

//    if (!string.IsNullOrEmpty(input))
//    {
//        query = query.Where(a => a.Artist.Name.Contains(input) ||
//                                 a.Name.Contains(input) ||
//                                 a.Album.Title.Contains(input) ||
//                                 a.MusicGenres.Any(mg => mg.Genre.Name.Contains(input)));
//    }

//    int totalCount = await query.CountAsync();
//    if (page.HasValue && perPage.HasValue)
//    {
//        int currentPage = page.Value > 0 ? page.Value : 1;
//        int itemsPerPage = perPage.Value > 0 ? perPage.Value : 10;

//        int totalPages = (int)Math.Ceiling((double)totalCount / itemsPerPage);
//        currentPage = currentPage > totalPages ? totalPages : currentPage;

//        int skip = Math.Max((currentPage - 1) * itemsPerPage, 0);

//        query = query.OrderBy(a => a.Name).Skip(skip).Take(itemsPerPage);
//    }
//    else
//    {
//        query = query.OrderBy(a => a.Name);
//    }

//    var music = await query.Select(x => new MusicGetDto
//    {
//        Id = x.Id,
//        Name = x.Name,
//        MusicUrl = x.MusicUrl,
//        Artistname = x.Artist.Name,
//        Album = new AlbumGetDto
//        {
//            Id = x.Album.Id,
//            Title = x.Album.Title,
//            CoverImage = x.Album.CoverImage,
//        },
//        Genre = new GenreGetDto
//        {
//            Id = x.MusicGenres.First().Genre.Id,
//            Name = x.MusicGenres.First().Genre.Name,
//        }
//    }).ToListAsync();

//    return Ok(music);
//}