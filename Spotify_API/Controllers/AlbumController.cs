using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spotify_API.Data;
using Spotify_API.DTO.Album;
using Spotify_API.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spotify_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AlbumController(AppDbContext appDbContext, IMapper mapper)
        {
            _context = appDbContext;
            _mapper = mapper;
        }
        // GET: api/<AlbumController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AlbumController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AlbumController>
        [HttpPost("AlbumPost")]
        public IActionResult Post([FromBody] AlbumPostDto dto)
        {

            if (!ModelState.IsValid) return BadRequest();

            var album = new Album();

            _mapper.Map(dto, album);
            _context.Albums.Add(album);
            _context.SaveChanges();
            return Ok(album.Id);
        }

        // PUT api/<AlbumController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AlbumController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
