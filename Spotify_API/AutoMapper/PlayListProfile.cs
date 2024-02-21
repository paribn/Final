using AutoMapper;
using Spotify_API.DTO.PlayList;
using Spotify_API.Entities;

namespace Spotify_API.AutoMapper
{
    public class PlayListProfile : Profile
    {
        public PlayListProfile()
        {
            CreateMap<PlayListPostDto, Playlist>()
                .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUserId))
                .ReverseMap();

            CreateMap<Playlist, PlaylistGetDto>().
                ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUserId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title)).ReverseMap();


            CreateMap<Playlist, PlaylistDetailDto>()
                .ForMember(dest => dest.musicGets, opt => opt.MapFrom(src => src.MusicPlayLists.Select(m => m.Music)));


            //CreateMap<Album, AlbumGetDetail>()
            //   .ForMember(dest => dest.Artistname, opt => opt.MapFrom(src => src.Artist.Name))
            //   .ForMember(dest => dest.musicAlbumGetDtos, opt => opt.MapFrom(src => src.Musics.Select(m => new MusicAlbumGetDto
            //   {
            //       Id = m.Id,
            //       MusicName = m.Name,
            //       MusicUrl = m.MusicUrl,
            //       MusicPhotoUrl = m.PhotoUrl
            //   })));
        }
    }
}
