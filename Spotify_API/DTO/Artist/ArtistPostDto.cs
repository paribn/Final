using FluentValidation;
using Spotify_API.Helpers.Enums;

namespace Spotify_API.DTO.Artist
{
    public class ArtistPostDto
    {
        public string Name { get; set; }
        public string About { get; set; }

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
               .Length(1, 255).WithMessage("Can't be less than 1  more than 255  characters!");

                RuleFor(x => x.ArtistTypes).NotNull();

            }

        }
    }
}
