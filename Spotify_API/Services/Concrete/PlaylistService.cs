using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spotify_API.Data;
using Spotify_API.DTO.PlayList;
using Spotify_API.Entities;
using Spotify_API.Services.Abstract;

namespace Spotify_API.Services.Concrete
{
    public class PlaylistService : IPlaylistService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PlaylistService(AppDbContext appDbContext, IMapper mapper)
        {
            _context = appDbContext;
            _mapper = mapper;
        }
        public async Task CreateAsync(PlayListPostDto playListPostDto)
        {
            var music = await _context.Musics.FirstOrDefaultAsync(x => playListPostDto.MusicId.Contains(x.Id));

            if (music != null)
            {
                var playlist = _mapper.Map<Playlist>(playListPostDto);

                playlist.MusicPlayLists = new List<MusicPlayList>();

                var musicplaylist = new MusicPlayList
                {
                    MusicId = music.Id,
                    PlayListId = playlist.Id,
                };

                playlist.MusicPlayLists.Add(musicplaylist);

                _context.Playlists.Add(playlist);
                await _context.SaveChangesAsync();
            }

        }
    }
}
