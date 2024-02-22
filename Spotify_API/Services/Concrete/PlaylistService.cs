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
            if (playListPostDto == null)
            {
                throw new ArgumentNullException(nameof(playListPostDto), "Playlist DTO cannot be null");
            }

            if (string.IsNullOrEmpty(playListPostDto.AppUserId))
            {
                throw new ArgumentException("AppUserId must be specified", nameof(playListPostDto.AppUserId));
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == playListPostDto.AppUserId);
            if (user == null)
            {
                throw new ArgumentException("User with the specified Id does not exist", nameof(playListPostDto.AppUserId));
            }

            if (playListPostDto.MusicId != null && playListPostDto.MusicId.Any())
            {
                var musics = await _context.Musics.Where(m => playListPostDto.MusicId.Contains(m.Id)).ToListAsync();
                if (musics.Count != playListPostDto.MusicId.Count)
                {
                    throw new ArgumentException("One or more specified MusicIds do not exist", nameof(playListPostDto.MusicId));
                }
            }

            var existingPlaylist = await _context.Playlists.FirstOrDefaultAsync(p => p.Title == playListPostDto.Title && p.AppUserId == playListPostDto.AppUserId);
            if (existingPlaylist != null)
            {
                throw new ArgumentException("Playlist with the same title already exists for the specified user", nameof(playListPostDto.Title));
            }

            var playlist = _mapper.Map<Playlist>(playListPostDto);
            playlist.AppUserId = playListPostDto.AppUserId;

            if (playListPostDto.MusicId != null && playListPostDto.MusicId.Any())
            {
                playlist.MusicPlayLists = playListPostDto.MusicId.Select(musicId => new MusicPlayList
                {
                    MusicId = musicId,
                    PlayListId = playlist.Id
                }).ToList();
            }

            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();
        }


        public async Task<List<PlaylistGetDto>> GetAsync(int? page = null, int? perPage = null, string playListName = null)
        {

            IQueryable<Playlist> query = _context.Playlists;

            if (!string.IsNullOrEmpty(playListName))
            {
                query = query.Where(p => p.Title.Contains(playListName));
            }

            if (page.HasValue && perPage.HasValue)
            {
                int totalCount = await query.CountAsync();
                int totalPages = (int)Math.Ceiling((double)totalCount / perPage.Value);
                page = Math.Min(Math.Max(page.Value, 1), totalPages);

                int skip = (page.Value - 1) * perPage.Value;

                query = query.Skip(skip).Take(perPage.Value);
            }

            var playlists = await query.ToListAsync();
            var playlistDtos = _mapper.Map<List<PlaylistGetDto>>(playlists);

            return playlistDtos;
        }

        public async Task<PlaylistDetailDto> GetDetailAsync(int id)
        {
            var genre = await _context.Playlists
                .Include(p => p.MusicPlayLists)
                .ThenInclude(x => x.Music)
                .ThenInclude(xx => xx.Artist)
                .FirstOrDefaultAsync(g => g.Id == id);


            var genreDetailDto = _mapper.Map<PlaylistDetailDto>(genre);

            return genreDetailDto;
        }

        public async Task DeleteAsync(int id)
        {
            var existingPlaylist = await _context.Playlists.FirstOrDefaultAsync(p => p.Id == id);
            if (existingPlaylist == null)
            {
                throw new ArgumentException("Playlist with the specified Id does not exist", nameof(id));
            }

            _context.Playlists.Remove(existingPlaylist);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(int id, PlaylistPutDto putDto)
        {
            var existingPlaylist = await _context.Playlists.FirstOrDefaultAsync(p => p.Id == id);
            if (existingPlaylist == null)
            {
                throw new ArgumentException("Playlist with the specified Id does not exist", nameof(id));
            }

            existingPlaylist.Title = putDto.Title;

            await _context.SaveChangesAsync();
        }

    }
}
