using FluentValidation;

namespace Spotify_API.DTO.Artist
{
    public class ArtistPhotoCreateDto
    {
        public List<IFormFile> PhotoPath { get; set; }
        public bool IsMain { get; set; }
        public int ArtistId { get; set; }


        public class ArtistPhotoDtoValidator : AbstractValidator<ArtistPhotoCreateDto>
        {
            public ArtistPhotoDtoValidator()
            {

                RuleFor(x => x.ArtistId)
                  .NotEmpty().WithMessage("Something went wrong")
                  .NotNull().WithMessage("Required!");

                RuleFor(xx => xx.PhotoPath)
                    .NotNull().WithMessage("The photo file cannot be empty.")
                    .Must(BeValidImageFiles).WithMessage("Invalid photo file type. Only image files are accepted.");

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
