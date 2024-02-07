using AutoMapper;
using Spotify_API.DTO.Account;
using Spotify_API.Entities;

namespace Spotify_API.AutoMapper
{
    public class Register : Profile
    {
        public Register()
        {
            CreateMap<RegisterDto, AppUser>().ReverseMap();

        }
    }
}
