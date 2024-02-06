﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spotify_API.Data;
using Spotify_API.DTO.PlayList;
using Spotify_API.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spotify_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayListController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PlayListController(AppDbContext appDbContext, IMapper mapper)
        {
            _context = appDbContext;
            _mapper = mapper;
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
        public IActionResult Post([FromBody] PlayListPostDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var playlist = new Playlist();

            _mapper.Map(dto, playlist);
            _context.Playlists.Add(playlist);
            _context.SaveChanges();
            return Ok(playlist.Id);

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
