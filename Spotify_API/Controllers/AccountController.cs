using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify_API.DTO.Account;
using Spotify_API.Entities;
using Spotify_API.Services.Abstract;
using System.Web;
using static Spotify_API.DTO.Account.RegisterDto;
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
        public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
        {
            try
            {
                RegisterDtoValidator validator = new();

                var validationResult = validator.Validate(registerDto);

                if (!validationResult.IsValid)
                {
                    var response = new ApiResponse
                    {
                        ErrorMessage = validationResult.Errors.Select(m => m.ErrorMessage).ToList(),
                        StatusMessage = "Failed"
                    };
                    return BadRequest(response);
                }

                await _accountService.RegisterAsync(registerDto);

                var user = await _userManager.FindByEmailAsync(registerDto.Email);

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                //var link = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token },
                //    Request.Scheme, Request.Host.ToString());

                //if (link == null) throw new NullReferenceException(nameof(link));

                //_emailService.Register(registerDto, link);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse { ErrorMessage = new List<string> { ex.Message } });
            }
        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            try
            {
                return Ok(await _accountService.LoginAsync(login));
            }
            catch (Exception)
            {
                return BadRequest("UserName or Password wrong.");
            }

        }


        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();

            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) return BadRequest();

            await _accountService.ConfirmEmailAsync(userId, token);

            return Redirect("http://localhost:5159/Login");
        }


        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordDto forgotPasswordDto)
        {
            try
            {
                var exsistUser = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
                if (exsistUser == null) return NotFound("User not found");

                var token = await _userManager.GeneratePasswordResetTokenAsync(exsistUser);

                var link = $"http://localhost:5159/ResetPassword?email={exsistUser.Email}&token={HttpUtility.UrlEncode(token)}";

                if (link == null) throw new NullReferenceException(nameof(link));

                _emailService.ForgotPassword(exsistUser, link, forgotPasswordDto);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpPost("ResetPassword")]
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

                return Redirect("http://localhost:5159/Login");
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse { ErrorMessage = new List<string> { ex.Message } });
            }
        }




    }
}
