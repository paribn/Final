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

        }
    }
}
