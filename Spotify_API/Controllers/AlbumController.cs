using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spotify_API.Data;
using Spotify_API.DTO.Album;
using Spotify_API.Services.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spotify_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {


        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAlbumService _albumService;
        public AlbumController(IAlbumService albumService, AppDbContext appDbContext, IMapper mapper)
        {
            _albumService = albumService;
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
        public async Task<IActionResult> Post([FromForm] AlbumPostDto dto)
        {
            try
            {
                await _albumService.CreateAsync(dto);
                return Ok("Album created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating album: {ex.Message}");
            }

        }

        // PUT api/<AlbumController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AlbumPutDto dto)
        {
            var option = _context.Albums.FirstOrDefault(x => x.Id == id);
            if (option is null) return NotFound("was not found");
            _mapper.Map(dto, option);

            _context.SaveChanges();
            return Ok(option.Id);
        }

        // DELETE api/<AlbumController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
