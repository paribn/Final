using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spotify_API.Data;
using Spotify_API.DTO.Music;
using Spotify_API.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spotify_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MusicController(AppDbContext appDbContext, IMapper mapper)
        {
            _context = appDbContext;
            _mapper = mapper;
        }

        // GET: api/<MusicController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MusicController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MusicController>
        [HttpPost]
        public IActionResult Post([FromBody] MusicPostDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var music = new Music();

            _mapper.Map(dto, music);
            _context.Musics.Add(music);
            _context.SaveChanges();
            return Ok(music.Id);
        }



        // PUT api/<MusicController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MusicController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
