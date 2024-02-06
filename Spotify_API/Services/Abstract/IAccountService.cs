using Spotify_API.DTO.Account;

namespace Spotify_API.Services.Abstract
{
    public interface IAccountService
    {
        Task<ApiResponse> ResetPassword(ResetPasswordDto resetPasswordDto);
        Task ConfirmEmailAsync(string userId, string token);


    }
}
