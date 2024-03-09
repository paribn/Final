using AutoMapper;
using Spotify_API.DTO.Album;
using Spotify_API.DTO.Music;
using Spotify_API.Entities;

namespace Spotify_API.AutoMapper
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile()
        {

            CreateMap<AlbumPostDto, Album>()
                          .ForMember(dest => dest.CoverImage, opt => opt.MapFrom(src => src.CoverImage))
                          .ForMember(dest => dest.ArtistId, opt => opt.MapFrom(src => src.ArtisId))
                          .ReverseMap();

            CreateMap<AlbumPutDto, Album>()
                            .ForMember(dest => dest.CoverImage, opt => opt.MapFrom(src => src.CoverImage)).ReverseMap();


            CreateMap<Album, AlbumGetDto>()
                .ForMember(dest => dest.Artistname, opt => opt.MapFrom(src => src.Artist.Name));

            CreateMap<Album, AlbumGetDetail>()
                .ForMember(dest => dest.Artistname, opt => opt.MapFrom(src => src.Artist.Name))
                .ForMember(dest => dest.musicAlbumGetDtos, opt => opt.MapFrom(src => src.Musics.Select(m => new MusicAlbumGetDto
                {
                    Id = m.Id,
                    MusicName = m.Name,
                    MusicUrl = m.MusicUrl,
                    MusicPhotoUrl = m.PhotoUrl,
                    AlbumName = m.Album.Title
                })));


        }
    }
}
