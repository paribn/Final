using AutoMapper;
using Spotify_API.DTO.Genre;
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
                .ForMember(dest => dest.ArtistType, opt => opt.MapFrom(src => src.Artist.ArtistType))
                .ForMember(dest => dest.AlbumName, opt => opt.MapFrom(src => src.Album.Title));

            //.ForMember(dest => dest.CatehoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<GenrePostDto, Genre>()
                 .ReverseMap();



        }
    }
}
