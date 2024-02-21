using FluentValidation;

namespace Spotify_API.DTO.Album
{
    public class AlbumPostDto
    {
        public string Title { get; set; }
        public IFormFile? CoverImage { get; set; }

        public int ArtisId { get; set; }



        public class AlbumPostDtoValidator : AbstractValidator<AlbumPostDto>
        {
            public AlbumPostDtoValidator()
            {
                RuleFor(x => x.Title)
                  .NotNull().WithMessage(" Title is required!")
                  .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Name must not consist only of white spaces!")
                  .Length(1, 100).WithMessage("Can't be less than 1  more than 100  characters!");

                RuleFor(x => x.ArtisId)
                  .NotEmpty().WithMessage("Something went wrong")
                  .NotNull().WithMessage("Required!");

                RuleFor(x => x.CoverImage)
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
