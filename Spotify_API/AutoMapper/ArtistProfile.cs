using AutoMapper;
using Spotify_API.DTO.Artist;
using Spotify_API.Entities;

namespace Spotify_API.AutoMapper
{
    public class ArtistProfile : Profile
    {
        public ArtistProfile()
        {
            //CreateMap<ArtistPostDto, Artist>().ReverseMap();

            // create artist post
            CreateMap<ArtistPostDto, Artist>()
            .ForMember(dest => dest.ArtistType, opt => opt.MapFrom(src => src.ArtistTypes))
            .ReverseMap();  /// burada yalnizz artissst add olur 

            CreateMap<ArtistPutDto, Artist>().ReverseMap();

        }

    }
}
