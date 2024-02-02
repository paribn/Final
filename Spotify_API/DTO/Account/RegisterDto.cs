using Spotify_API.Helpers.Enum;

namespace Spotify_API.DTO.Account
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Password { get; set; }
    }
}
