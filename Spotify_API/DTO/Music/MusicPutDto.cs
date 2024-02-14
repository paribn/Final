using FluentValidation;

namespace Spotify_API.DTO.Music
{
    public class MusicPutDto
    {
        public string Title { get; set; }
        public int Duration { get; set; }
        public IFormFile? PhotoUrl { get; set; }

        public class MusicDtoValidator : AbstractValidator<MusicPutDto>
        {
            public MusicDtoValidator()
            {
                //RuleFor(x => x.Id)
                //   .NotEmpty().WithMessage("Something went wrong")
                //   .NotNull().WithMessage("Required!");

                RuleFor(x => x.Title)
                .MinimumLength(1).WithMessage("Your length must be at least 1.")
                .MaximumLength(255).WithMessage("Your  length must not exceed 255.");

                RuleFor(x => x.Duration)
                .NotNull().WithMessage("Duration is required!");

                RuleFor(music => music.PhotoUrl)
                   .NotNull().WithMessage("The photo file cannot be empty.")
                   .Must(BeAValidImageFile).WithMessage("Invalid photo file type. Only image files are accepted.");
            }

            private bool BeAValidImageFile(IFormFile file)
            {
                if (file == null) return true;

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
                return allowedExtensions.Contains(fileExtension);
            }
        }

    }
}
