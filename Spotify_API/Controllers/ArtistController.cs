using Microsoft.AspNetCore.Mvc;
using Spotify_API.DTO.Artist;
using Spotify_API.Services.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spotify_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artist;

        public ArtistController(IArtistService artistService)
        {
            _artist = artistService;
        }


        // GET: api/<ArtistController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _artist.GetAllAsync());
            }
            catch (Exception)
            {
                return NotFound("No artist found!");
            }
        }

        // GET api/<ArtistController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _artist.GetDetailAsync(id));
            }
            catch (Exception)
            {
                return NotFound("No artist found!");
            }
        }

        // POST api/<ArtistController>
        [HttpPost("CreateArtist")]
        public async Task<IActionResult> Post([FromBody] ArtistPostDto dto)
        {
            try
            {
                await _artist.CreateAsync(dto);
                return Ok("Artist created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating artist: {ex.Message}");
            }
        }

        // PUT api/<ArtistController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ArtistPutDto dto)
        {
            try
            {
                await _artist.UpdateAsync(id, dto);
                return Ok("change");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/<ArtistController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _artist.DeleteAsync(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }



        [HttpPost("CreateArtistPhoto")]
        public async Task<IActionResult> AddPhoto([FromForm] ArtistPhotoCreateDto dto)
        {
            try
            {
                await _artist.AddPhoto(dto);
                return Ok("Artist created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating artist: {ex.Message}");
            }
        }


        [HttpPut("UpdateArtistPhoto")]
        public async Task<IActionResult> UpdatePhoto(int id, [FromForm] ArtistPhotoUpdateDto photoDto)
        {
            try
            {
                await _artist.UpdatePhoto(id, photoDto);
                return Ok("Artist created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating artist: {ex.Message}");
            }
        }
    }
}
