using FluentValidation;

namespace Spotify_API.DTO.Music
{
    public class MusicPostDto
    {
        public string Name { get; set; }
        public IFormFile? MusicUrl { get; set; }
        public IFormFile? PhotoUrl { get; set; }

        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public List<int>? GenreId { get; set; }

        public class MusicPostDtoValidator : AbstractValidator<MusicPostDto>
        {
            public MusicPostDtoValidator()
            {
                RuleFor(x => x.Name)
                  .NotNull().WithMessage(" Title is required!")
                  .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Name must not consist only of white spaces!")
                  .Length(1, 100).WithMessage("Can't be less than 1  more than 100  characters!");


                RuleFor(music => music.MusicUrl)
                .NotNull().WithMessage("Music is not empty.")
                 .Must(BeAValidMusicFile).WithMessage("Invalid file type. Only .mp3 files are accepted");

                RuleFor(x => x.ArtistId)
                  .NotEmpty().WithMessage("Something went wrong")
                  .NotNull().WithMessage("Required!");

                RuleFor(x => x.AlbumId)
                  .NotEmpty().WithMessage("Something went wrong")
                  .NotNull().WithMessage("Required!");

                RuleFor(music => music.PhotoUrl)
                    .NotNull().WithMessage("The photo file cannot be empty.")
                    .Must(BeAValidImageFile).WithMessage("Invalid photo file type. Only image files are accepted.");

                RuleFor(x => x.GenreId)
                  .NotEmpty().WithMessage("Something went wrong")
                  .NotNull().WithMessage("Required!");

            }

            private bool BeAValidMusicFile(IFormFile file)
            {
                if (file == null) return true;

                var allowedExtensions = new[] { ".mp3" };
                var fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
                return allowedExtensions.Contains(fileExtension);
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
