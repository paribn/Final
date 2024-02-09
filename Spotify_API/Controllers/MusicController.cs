using Microsoft.AspNetCore.Mvc;
using Spotify_API.DTO.Music;
using Spotify_API.Services.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spotify_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService _musicSercive;

        public MusicController(IMusicService musicService)
        {
            _musicSercive = musicService;
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
        public async Task<IActionResult> Post([FromForm] MusicPostDto dto)
        {
            try
            {
                await _musicSercive.CreateAsync(dto);
                return Ok("Music created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating music: {ex.Message}");
            }
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
