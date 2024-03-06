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
        public async Task<IActionResult> Get(int? page = null, int? perPage = null, string playListName = null)
        {
            try
            {
                var playlist = await _playlistService.GetAsync(page, perPage, playListName);
                return Ok(playlist);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating: {ex.Message}");
            }
        }

        [HttpGet("GetUserPlaylist")]
        public async Task<IActionResult> GetUser(int? page = null, int? perPage = null, string playListName = null, string userId = null)
        {
            try
            {
                var playlist = await _playlistService.PlaylistGetUser(page, perPage, playListName, userId);
                return Ok(playlist);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating: {ex.Message}");
            }
        }

        // GET api/<PlayListController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _playlistService.GetDetailAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating: {ex.Message}");
            }

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
        public async Task<IActionResult> Put(int id, [FromBody] PlaylistPutDto putDto)
        {
            try
            {
                await _playlistService.UpdateAsync(id, putDto);
                return Ok("Playlist updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating: {ex.Message}");
            }
        }

        // DELETE api/<PlayListController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _playlistService.DeleteAsync(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating: {ex.Message}");
            }

        }
    }
}
