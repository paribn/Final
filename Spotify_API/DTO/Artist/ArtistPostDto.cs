using FluentValidation;
using Spotify_API.Helpers.Enums;

namespace Spotify_API.DTO.Artist
{
    public class ArtistPostDto
    {
        public string Name { get; set; }
        public string About { get; set; }

        public List<IFormFile> PhotoPath { get; set; }

        public ArtistTypes ArtistTypes { get; set; }

        public class ArtistPostDtoValidator : AbstractValidator<ArtistPostDto>
        {
            public ArtistPostDtoValidator()
            {
                RuleFor(x => x.Name)
                  .NotNull().WithMessage(" Name is required!")
                  .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Name must not consist only of white spaces!")
                  .Length(1, 100).WithMessage("Can't be less than 1  more than 100  characters!");

                RuleFor(x => x.About)
               .NotNull().WithMessage(" About is required!")
               .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("About must not consist only of white spaces!")
               .Length(1, 1000).WithMessage("Can't be less than 1  more than 1000  characters!");

                RuleFor(xx => xx.PhotoPath)
                 .NotNull().WithMessage("The photo file cannot be empty.")
                 .Must(BeValidImageFiles).WithMessage("Invalid photo file type. Only image files are accepted.");

                RuleFor(x => x.ArtistTypes).NotNull();

            }
            private bool BeValidImageFiles(List<IFormFile> files)
            {
                foreach (var file in files)
                {
                    if (file == null) continue;

                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
                    if (!allowedExtensions.Contains(fileExtension))
                        return false;
                }
                return true;
            }

        }
    }
}
