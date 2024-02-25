using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Spotify_API.DTO.Account;
using Spotify_API.Entities;
using Spotify_API.Helpers.Enum;
using Spotify_API.Services.Abstract;

namespace Spotify_API.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signManager;
        private readonly IJwtTokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
                              IJwtTokenService tokenService,

            IMapper mapper)
        {
            _userManager = userManager;
            _signManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }


        public async Task ConfirmEmailAsync(string userId, string token)
        {
            if (userId == null && token == null) throw new ArgumentNullException();

            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user == null) throw new NullReferenceException();

            await _userManager.ConfirmEmailAsync(user, token);
        }


        public async Task<ApiResponse> RegisterAsync(RegisterDto registerDto)
        {
            var user = _mapper.Map<AppUser>(registerDto);

            if (user == null) throw new NullReferenceException();

            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return new ApiResponse
                {
                    ErrorMessage = result.Errors.Select(m => m.Description).ToList(),
                    StatusMessage = "Failed"
                };
            }

            await _userManager.AddToRoleAsync(user, Roles.User.ToString());

            return new ApiResponse { ErrorMessage = null, StatusMessage = "Success" };
        }


        public async Task LogoutAsync()
        {
            await _signManager.SignOutAsync();
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



        //public async Task<string?> LoginAsync(LoginDto loginDto)
        //{
        //    var user = await _userManager.FindByEmailAsync(loginDto.Email);

        //    if (!await _userManager.CheckPasswordAsync(user, loginDto.Password)) return null;

        //    var roles = await _userManager.GetRolesAsync(user);

        //    string token = _tokenService.GenerateToken(user.Email, user.UserName, (List<string>)roles);

        //    return new { Email = user.Email, Token = token };
        //}

        public async Task<object> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return new { Email = (string)null, Token = (string)null };

            var roles = await _userManager.GetRolesAsync(user);

            string token = _tokenService.GenerateToken(user.Email, user.UserName, (List<string>)roles);

            return new { Email = user.Email, Token = token };
        }

    }
}
