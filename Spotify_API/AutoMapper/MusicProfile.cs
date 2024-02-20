using AutoMapper;
using Spotify_API.DTO.Music;
using Spotify_API.Entities;

namespace Spotify_API.AutoMapper
{
    public class MusicProfile : Profile
    {
        public MusicProfile()
        {
            CreateMap<MusicPostDto, Music>().ReverseMap();

            CreateMap<Music, MusicGetDto>().ForMember(dest => dest.Artistname, opt => opt.MapFrom(src => src.Artist.Name))
                .ForMember(dest => dest.ArtistType, opt => opt.MapFrom(src => src.Artist.ArtistType));


            CreateMap<Music, MusicGetDetail>().ForMember(dest => dest.Artistname, opt => opt.MapFrom(src => src.Artist.Name))
                .ForMember(dest => dest.About, opt => opt.MapFrom(src => src.Artist.About))
                .ForMember(dest => dest.ArtistType, opt => opt.MapFrom(src => src.Artist.ArtistType))
                .ForMember(dest => dest.AlbumName, opt => opt.MapFrom(src => src.Album.Title));


            CreateMap<MusicPutDto, Music>().ReverseMap();





        }
    }
}
