using Microsoft.AspNetCore.Mvc;
using Spotify_API.DTO.PlayList;
using Spotify_API.Services.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spotify_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayListController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;

        public PlayListController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }


        // GET: api/<PlayListController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PlayListController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PlayListController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] PlayListPostDto dto)
        {
            try
            {
                await _playlistService.CreateAsync(dto);
                return Ok("Playlist created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating: {ex.Message}");
            }
        }

        // PUT api/<PlayListController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PlayListController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
