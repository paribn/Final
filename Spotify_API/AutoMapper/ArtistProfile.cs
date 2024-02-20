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

            CreateMap<Artist, ArtistGetDto>().
                ForMember(dest => dest.artistPhotos, opt => opt.MapFrom(src => src.ArtistPhoto.Select(x => new ArtistPhotoGetDto
                {
                    Id = x.Id,
                    PhotoPath = x.PhotoPath
                })));




            //.ForMember(dest => dest.musicAlbumGetDtos, opt => opt.MapFrom(src => src.Musics.Select(m => new MusicAlbumGetDto
            // {
            //     Id = m.Id,
            //     MusicName = m.Name,
            //     MusicUrl = m.MusicUrl,
            //     MusicPhotoUrl = m.PhotoUrl
            // })));

            CreateMap<Artist, ArtistGetDetail>()
           .ForMember(dest => dest.Albums, opt => opt.MapFrom(src => src.Albums));



            //CreateMap<Artist, ArtistPhotoCreateDto>()
            //    .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.ArtistPhoto)).ReverseMap();

            //CreateMap<ArtistPhotoCreateDto, Artist>()
            //         .ForMember(dest => dest.ArtistPhoto, opt => opt.MapFrom(src => src.PhotoPath)).ReverseMap();
            CreateMap<ArtistPhotoCreateDto, ArtistPhoto>()
                .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.PhotoPath));


        }

    }
}
