using FluentValidation;
using Spotify_API.Helpers.Enum;

namespace Spotify_API.DTO.Account
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Password { get; set; }

        public class RegisterDtoValidator : AbstractValidator<RegisterDto>
        {
            public RegisterDtoValidator()
            {
                RuleFor(u => u.FullName).NotNull().NotEmpty().Length(5, 50);
                RuleFor(u => u.Email).NotNull().NotEmpty().Length(10, 50).EmailAddress();
                RuleFor(u => u.Password).NotNull().NotEmpty().Length(5, 12);
                RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Invalid gender.");
            }
        }
    }
}
