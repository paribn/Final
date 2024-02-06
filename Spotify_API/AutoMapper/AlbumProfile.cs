using AutoMapper;
using Spotify_API.DTO.Album;
using Spotify_API.Entities;

namespace Spotify_API.AutoMapper
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile()
        {

            CreateMap<AlbumPostDto, Album>()
                          .ForMember(dest => dest.ArtistId, opt => opt.MapFrom(src => src.ArtisId))
                          .ReverseMap();

        }
    }
}
