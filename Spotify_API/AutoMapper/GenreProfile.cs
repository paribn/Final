using AutoMapper;
using Spotify_API.DTO.Genre;
using Spotify_API.DTO.Music;
using Spotify_API.Entities;

namespace Spotify_API.AutoMapper
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<GenrePostDto, Genre>()
          .ReverseMap();


            CreateMap<Genre, GenreGetDetail>()
            .ForMember(dest => dest.Musics, opt => opt.MapFrom(src => src.MusicGenres.Select(mg => mg.Music)))
            .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.MusicGenres.Select(x => x.Music.Artist)));



            CreateMap<Genre, GenreGetDto>().ReverseMap();
            CreateMap<Genre, GenrePutDto>().ReverseMap();

            CreateMap<Genre, MusicPostDto>()
           .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.PhotoPath));

        }
    }
}
