using Spotify_API.DTO.Account;
using Spotify_API.Entities;

namespace Spotify_API.Services.Abstract
{
    public interface IEmailService
    {
        public void ForgotPassword(AppUser user, string link, ForgotPasswordDto forgotPasswordDto);
        //public void Register(RegisterDto registerDto, string link);

    }
}
