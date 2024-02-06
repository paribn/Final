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


            CreateMap<GenrePostDto, Genre>()
                 .ReverseMap();



        }
    }
}
