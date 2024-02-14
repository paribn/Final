﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _musicSercive.GetAsync());
            }
            catch (Exception)
            {
                return NotFound("No music found!");
            }
        }

        // GET api/<MusicController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _musicSercive.GetDetailAsync(id));
            }
            catch (Exception)
            {
                return NotFound("No music found!");
            }
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
        public async Task<IActionResult> Put(int id, [FromForm] MusicPutDto dto)
        {
            try
            {
                await _musicSercive.UpdateAsync(id, dto);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(new { ErrorMessage = "Not Updated" });
            }
        }

        // DELETE api/<MusicController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _musicSercive.DeleteAsync(id);

                return Ok("Deleted successfully ");
            }
            catch (Exception)
            {
                return NotFound("Music not found!");
            }
        }
    }
}
