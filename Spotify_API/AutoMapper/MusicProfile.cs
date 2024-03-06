using AutoMapper;
using Spotify_API.DTO.Music;
using Spotify_API.Entities;

namespace Spotify_API.AutoMapper
{
    public class MusicProfile : Profile
    {
        public MusicProfile()
        {
            CreateMap<MusicPostDto, Music>()
               .ForMember(dest => dest.MusicGenres, opt => opt.MapFrom(src =>
                src.GenreId.Select(genreId => new MusicGenre { GenreId = genreId })));


            //CreateMap<Music, MusicGetDto>().ReverseMap();

            CreateMap<Music, MusicGetDto>()
           .ForMember(dest => dest.Artistname, opt => opt.MapFrom(src => src.Artist.Name))
           .ForMember(dest => dest.ArtistId, opt => opt.MapFrom(src => src.Artist.Id));


            CreateMap<Music, MusicGetDetail>().ForMember(dest => dest.Artistname, opt => opt.MapFrom(src => src.Artist.Name))
                .ForMember(dest => dest.About, opt => opt.MapFrom(src => src.Artist.About))
                .ForMember(dest => dest.ArtistType, opt => opt.MapFrom(src => src.Artist.ArtistType))
                .ForMember(dest => dest.AlbumName, opt => opt.MapFrom(src => src.Album.Title));


            CreateMap<Music, MusicAlbumGetDto>().
                ForMember(dest => dest.MusicName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.MusicPhotoUrl, opt => opt.MapFrom(src => src.PhotoUrl))
               .ForMember(dest => dest.AlbumName, opt => opt.MapFrom(src => src.Album.Title));



            CreateMap<MusicPutDto, Music>().ReverseMap();





        }
    }
}
