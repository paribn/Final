using FluentValidation;

namespace Spotify_API.DTO.Artist
{
    public class ArtistPhotoUpdateDto
    {
        public List<IFormFile> PhotoPath { get; set; }
        public bool IsMain { get; set; }

        public class ArtistPhotoDtoValidator : AbstractValidator<ArtistPhotoUpdateDto>
        {
            public ArtistPhotoDtoValidator()
            {

                RuleFor(xx => xx.PhotoPath)
                    .NotNull().WithMessage("The photo file cannot be empty.")
                    .Must(BeAValidImageFile).WithMessage("Invalid photo file type. Only image files are accepted.");

            }

            private bool BeAValidImageFile(List<IFormFile> files)
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
