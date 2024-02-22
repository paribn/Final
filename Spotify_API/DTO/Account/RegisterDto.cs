﻿using FluentValidation;
using Spotify_API.Helpers.Enum;
using System.Text.RegularExpressions;

namespace Spotify_API.DTO.Account
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public string? Username { get; set; }

        public int Age { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Password { get; set; }
        public string? ConfirmPassword { get; set; }

        public class RegisterDtoValidator : AbstractValidator<RegisterDto>
        {
            public RegisterDtoValidator()
            {
                RuleFor(u => u.FullName).NotNull().NotEmpty().Length(5, 50);

                RuleFor(x => x.Username)
               .NotEmpty().WithMessage("UserName field is required!")
               .Must(x => !IsEmailValid(x)).WithMessage("UserName cannot be an email address.");

                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email field is required!")
                    .EmailAddress().WithMessage("Invalid email format.")
                    .Must(x => IsEmailValid(x)).WithMessage("Invalid email format. Email should end with '.com' or '.ru'.");


                RuleFor(u => u.Age).GreaterThanOrEqualTo(12).WithMessage("You are under the age limit to create a account");

                RuleFor(x => x.Password)
                 .NotEmpty().WithMessage("Password field is required!")
                 .MinimumLength(4).WithMessage("Password must be at least 4 characters long.")
                 .Must(x => HasLetterAndDigit(x)).WithMessage("Password must contain at least one letter and one digit.");

                RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Invalid gender.");
            }

            private bool IsEmailValid(string value)
            {
                return Regex.IsMatch(value, @"@[a-zA-Z0-9\-\.]+\.(com|ru)$");
            }
            private bool HasLetterAndDigit(string value)
            {
                return Regex.IsMatch(value, @"^(?=.*[a-zA-Z])(?=.*\d).+$");
            }
        }
    }
}
