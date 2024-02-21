using FluentValidation;

namespace Spotify_API.DTO.Artist
{
    public class ArtistPhotoUpdateDto
    {
        public IFormFile? PhotoPath { get; set; }

        public class ArtistPhotoDtoValidator : AbstractValidator<ArtistPhotoUpdateDto>
        {
            public ArtistPhotoDtoValidator()
            {

                RuleFor(xx => xx.PhotoPath)
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
