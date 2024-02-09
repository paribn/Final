using Microsoft.AspNetCore.Mvc;
using Spotify_API.DTO.Genre;
using Spotify_API.Services.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spotify_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }


        // GET: api/<GenreController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<GenreController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GenreController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GenrePostDto dto)
        {
            try
            {
                await _genreService.CreateAsync(dto);
                return Ok("Genre created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating genre: {ex.Message}");
            }
        }

        // PUT api/<GenreController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GenreController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _genreService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
