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
        public async Task<IActionResult> GetAll(int? page = null, int? perPage = null, string genreName = null)
        {
            try
            {
                return Ok(await _genreService.GetAllAsync(page, perPage, genreName));
            }
            catch (Exception)
            {
                return NotFound("No genre found!");
            }
        }

        // GET api/<GenreController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _genreService.GetDetailAsync(id));
            }
            catch (Exception)
            {
                return NotFound($"Genre with id {id} not found");
            }
        }


        // POST api/<GenreController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] GenrePostDto dto)
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
        public async Task<IActionResult> Put(int id, [FromForm] GenrePutDto dto)
        {
            try
            {
                await _genreService.UpdateAsync(id, dto);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(new { ErrorMessage = "Not Updated" });
            }
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
                return NotFound($"Genre with id {id} not found");
            }
        }
    }
}
