namespace Spotify_API.Services.Abstract
{
    public interface IJwtTokenService
    {
        public string GenerateToken(string fullname, string username, List<string> Roles);

    }
}
