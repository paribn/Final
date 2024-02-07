using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spotify_API.Data;
using Spotify_API.DTO.Artist;
using Spotify_API.Entities;
using Spotify_API.Services.Abstract;

namespace Spotify_API.Services.Concrete
{
    public class ArtistService : IArtistService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ArtistService(AppDbContext appDbContext, IMapper mapper)
        {
            _context = appDbContext;
            _mapper = mapper;
        }

        public async Task CreateAsync(ArtistPostDto artistPostDto)
        {
            var artist = _mapper.Map<Artist>(artistPostDto);

            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, ArtistPutDto artistPutDto)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(x => x.Id == id);
            if (artist == null)
            {
                throw new Exception("artist tapilmadi");
            }

            _mapper.Map(artistPutDto, artist);

            await _context.SaveChangesAsync();



        }
    }
}
