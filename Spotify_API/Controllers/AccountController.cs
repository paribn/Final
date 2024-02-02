using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify_API.DTO.Account;
using Spotify_API.Entities;
using Spotify_API.Helpers.Enum;
using Spotify_API.Services.Abstract;
using System.Web;
using static Spotify_API.DTO.Account.ResetPasswordDto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spotify_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signManager;
        private readonly IEmailService _emailService;
        private readonly IAccountService _accountService;

        public AccountController(UserManager<AppUser> userManager, IAccountService accountService,
            SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signManager = signInManager;
            _emailService = emailService;
            _accountService = accountService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = new AppUser
            {
                FullName = registerDto.FullName,
                Gender = registerDto.Gender,
                BirthDate = registerDto.BirthDate,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest();

            await _userManager.AddToRoleAsync(user, Roles.Artist.ToString()); //// artist user yaranmayibb ,

            return Ok(result);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login, [FromServices] IJwtTokenService jwtTokenService)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null) return NotFound();

            var CheckPassword = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!CheckPassword) return NotFound();


            var roles = (await _userManager.GetRolesAsync(user)).ToList();

            var token = jwtTokenService.GenerateToken(user.FullName, user.UserName, roles);



            return Ok(token);

        }


        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();

            return Ok();
        }



        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordDto forgotPasswordDto)
        {
            try
            {
                var exsistUser = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
                if (exsistUser == null) return NotFound("User not found");

                var token = await _userManager.GeneratePasswordResetTokenAsync(exsistUser);

                var link = $"http://localhost:3000/ResetPassword?email={exsistUser.Email}&token={HttpUtility.UrlEncode(token)}";

                if (link == null) throw new NullReferenceException(nameof(link));

                _emailService.ForgotPassword(exsistUser, link, forgotPasswordDto);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto resetPasswordDto)
        {
            try
            {
                ResetPasswordValidator validator = new();

                var validationResult = validator.Validate(resetPasswordDto);

                if (!validationResult.IsValid)
                {
                    var response = new ApiResponse
                    {
                        ErrorMessage = validationResult.Errors.Select(m => m.ErrorMessage).ToList(),
                    };
                    return BadRequest(response);
                }

                await _accountService.ResetPassword(resetPasswordDto);

                return Redirect("http://localhost:3000/Login");
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse { ErrorMessage = new List<string> { ex.Message } });
            }
        }




    }
}
