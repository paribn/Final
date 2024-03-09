using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spotify_API.Data;
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
        public async Task<ActionResult> SearchAsync(string searchTerm = null, int? page = null, int? perPage = null)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("A search term must be provided.");
            }

            IQueryable<Music> query = _context.Musics
                .Include(x => x.Album)
                .Include(x => x.Artist)
                .Include(x => x.Artist.ArtistPhoto)
                .Include(x => x.MusicGenres)
                .ThenInclude(x => x.Genre);

            query = query.Where(a => a.Name.Contains(searchTerm) ||
                                     a.Album.Title.Contains(searchTerm) ||
                                      a.Artist.Name.Contains(searchTerm)).Take(1);




            if (query == null)
            {
                return BadRequest("A search term must be provided.");
            }
            int totalCount = await query.CountAsync();
            if (page.HasValue && perPage.HasValue)
            {
                int currentPage = page.Value > 0 ? page.Value : 1;
                int itemsPerPage = perPage.Value > 0 ? perPage.Value : 10;

                int totalPages = (int)Math.Ceiling((double)totalCount / itemsPerPage);
                currentPage = currentPage > totalPages ? totalPages : currentPage;

                int skip = Math.Max((currentPage - 1) * itemsPerPage, 0);

                query = query.OrderBy(a => a.Name).Skip(skip).Take(itemsPerPage);
            }
            else
            {
                query = query.OrderBy(a => a.Name);
            }

            var music = await query.Select(x => new MusicGetDto
            {
                Id = x.Id,
                Name = x.Name,
                MusicUrl = x.MusicUrl,
                PhotoUrl = x.PhotoUrl,
                //Artistname = x.Artist.Name,
                //ArtistId = x.Artist.Id,
                //ArtistPhoto = x.Artist.ArtistPhoto != null ? x.Artist.ArtistPhoto.Select(photo => new ArtistPhotoGetDto
                //{
                //    Id = photo.Id,
                //    PhotoPath = photo.PhotoPath
                //}).ToList() : null,
                //Album = new AlbumGetDto
                //{
                //    Id = x.Album.Id,
                //    Title = x.Album.Title,
                //    CoverImage = x.Album.CoverImage,
                //}
            }).ToListAsync();

            return Ok();
        }




    }
}
