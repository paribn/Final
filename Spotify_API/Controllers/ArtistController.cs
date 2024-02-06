﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spotify_API.Data;
using Spotify_API.DTO.Artist;
using Spotify_API.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spotify_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ArtistController(AppDbContext appDbContext, IMapper mapper)
        {
            _context = appDbContext;
            _mapper = mapper;
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
        public IActionResult Post([FromBody] ArtistPostDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var artist = new Artist();

            _mapper.Map(dto, artist);
            _context.Artists.Add(artist);
            _context.SaveChanges();
            return Ok(artist.Id);
        }

        // PUT api/<ArtistController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ArtistPutDto dto)
        {
            var option = _context.Artists.FirstOrDefault(x => x.Id == id);
            if (option is null) return NotFound("was not found");

            _mapper.Map(dto, option);

            _context.SaveChanges();
            return Ok(option.Id);
        }

        // DELETE api/<ArtistController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
