using FluentValidation;

namespace Spotify_API.DTO.Account
{
    public class ResetPasswordDto
    {
        public string? NewPassword { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }

        public class ResetPasswordValidator : AbstractValidator<ResetPasswordDto>
        {
            public ResetPasswordValidator()
            {
                RuleFor(u => u.NewPassword).NotNull().NotEmpty().Length(6, 12);
            }
        }
    }
}
