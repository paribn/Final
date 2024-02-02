using Microsoft.AspNetCore.Identity;
using Spotify_API.DTO.Account;
using Spotify_API.Entities;
using Spotify_API.Services.Abstract;

namespace Spotify_API.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signManager;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signManager = signInManager;
        }
        public async Task<ApiResponse> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var existUser = await _userManager.FindByEmailAsync(resetPasswordDto.Email);

            if (existUser == null) throw new NullReferenceException();

            if (await _userManager.CheckPasswordAsync(existUser, resetPasswordDto.NewPassword))
            {
                return new ApiResponse
                {
                    ErrorMessage = new List<string> { "Your password already exists!" },
                };
            }

            await _userManager.ResetPasswordAsync(existUser, resetPasswordDto.Token, resetPasswordDto.NewPassword);

            return new ApiResponse { ErrorMessage = null };

        }

    }
}
