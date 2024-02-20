using Microsoft.AspNetCore.Mvc;
using Spotify_API.DTO.Album;
using Spotify_API.Services.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spotify_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {


        private readonly IAlbumService _albumService;
        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }
        // GET: api/<AlbumController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _albumService.GetAllAsync());
            }
            catch (Exception)
            {
                return NotFound("No album found!");
            }
        }

        // GET api/<AlbumController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _albumService.GetDetailAsync(id));
            }
            catch (Exception)
            {
                return NotFound("No album found!");
            }
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

        //// PUT api/<AlbumController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] AlbumPutDto dto)
        {
            try
            {
                await _albumService.UpdateAsync(id, dto);

                return Ok("Successfully updated");
            }
            catch (Exception)
            {
                return NotFound($"Album with id {id} not found");
            }
        }

        // DELETE api/<AlbumController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _albumService.DeleteAsync(id);

                return Ok("Successfully deleted");
            }
            catch (Exception)
            {
                return NotFound($"Album with id {id} not found");
            }
        }
    }
}
