﻿using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ArtistController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
                return Ok("change"); // Başarılı işlem durumunda 200 OK yanıtı
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // Sanatçı bulunamadığı durumda 404 Not Found yanıtı
            }
        }

        // DELETE api/<ArtistController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
