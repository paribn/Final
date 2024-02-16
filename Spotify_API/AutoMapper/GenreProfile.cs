using AutoMapper;
using Spotify_API.DTO.Genre;
using Spotify_API.Entities;

namespace Spotify_API.AutoMapper
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<GenrePostDto, Genre>()
          .ReverseMap();
            //.ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));

            //CreateMap<GenreGetDto, Genre>().ForMember(dest => dest.MusicGenres, opt => opt.MapFrom(src => src.Musics)).ReverseMap();

            CreateMap<Genre, GenreGetDetail>()
             .ForMember(dest => dest.Musics, opt => opt.MapFrom(src => src.MusicGenres.Select(mg => mg.Music)));

            CreateMap<Genre, GenreGetDto>().ReverseMap();
            CreateMap<Genre, GenrePutDto>().ReverseMap();
        }
    }
}
