using FluentValidation;

namespace Spotify_API.DTO.Genre
{
    public class GenrePutDto
    {
        public string Name { get; set; }

        public IFormFile photoPath { get; set; }

        public class GenrePutDtoValidator : AbstractValidator<GenrePutDto>
        {
            public GenrePutDtoValidator()
            {
                RuleFor(x => x.Name)
                  .NotNull().WithMessage(" Title is required!")
                  .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Name must not consist only of white spaces!")
                  .Length(1, 100).WithMessage("Can't be less than 1  more than 100  characters!");


                RuleFor(genre => genre.photoPath)
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
