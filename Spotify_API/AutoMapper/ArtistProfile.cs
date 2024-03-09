﻿using AutoMapper;
using Spotify_API.DTO.Album;
using Spotify_API.DTO.Artist;
using Spotify_API.DTO.Music;
using Spotify_API.Entities;

namespace Spotify_API.AutoMapper
{
    public class ArtistProfile : Profile
    {
        public ArtistProfile()
        {
            // create artist post
            CreateMap<ArtistPostDto, Artist>()
            .ForMember(dest => dest.ArtistType, opt => opt.MapFrom(src => src.ArtistTypes))
            .ForMember(dest => dest.ArtistPhoto, opt => opt.MapFrom(src => src.PhotoPath.Select(p => new ArtistPhoto { PhotoPath = p.FileName }).ToList()))
            .ReverseMap();

            CreateMap<ArtistPutDto, Artist>().ReverseMap();

            CreateMap<Artist, ArtistGetDto>().
                ForMember(dest => dest.artistPhotos, opt => opt.MapFrom(src => src.ArtistPhoto.Select(x => new ArtistPhotoGetDto
                {
                    Id = x.Id,
                    PhotoPath = x.PhotoPath
                })));

            CreateMap<ArtistPhoto, ArtistPhotoGetDto>();


            CreateMap<Artist, ArtistGetDetail>()
                .ForMember(dest => dest.artistPhotos, opt => opt.MapFrom(src => src.ArtistPhoto.Select(x => new ArtistPhotoGetDto
                {
                    Id = x.Id,
                    PhotoPath = x.PhotoPath
                })))
                .ForMember(dest => dest.albumGets, opt => opt.MapFrom(src => src.Albums.Select(x => new AlbumGetDtoForArtist
                {
                    Id = x.Id,
                    Title = x.Title,
                    CoverImage = x.CoverImage,

                })))
                .ForMember(dest => dest.MusicGet, opt => opt.MapFrom(src => src.Musics.Select(x => new MusicAlbumGetDto
                {
                    Id = x.Id,
                    MusicName = x.Name,
                    MusicPhotoUrl = x.PhotoUrl,
                    MusicUrl = x.MusicUrl,
                    AlbumName = x.Album.Title
                })));


        }

    }
}
