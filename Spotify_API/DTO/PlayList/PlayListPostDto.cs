using FluentValidation;

namespace Spotify_API.DTO.PlayList
{
    public class PlayListPostDto
    {
        public string Title { get; set; }
        public string AppUserId { get; set; }

        public List<int>? MusicId { get; set; }


        public class PlaylistPostDtoValidator : AbstractValidator<PlayListPostDto>
        {
            public PlaylistPostDtoValidator()
            {
                RuleFor(x => x.Title)
                  .NotNull().WithMessage(" Title is required!")
                  .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Name must not consist only of white spaces!")
                  .Length(1, 100).WithMessage("Can't be less than 1  more than 100  characters!");


                //RuleFor(genre => genre.AppUserId)
                //    .NotNull().WithMessage("Cannot be empty.");

            }

        }

    }
}
