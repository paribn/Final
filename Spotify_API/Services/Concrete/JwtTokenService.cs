using Microsoft.IdentityModel.Tokens;
using Spotify_API.Services.Abstract;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Spotify_API.Services.Concrete
{
    public class JwtTokenService : IJwtTokenService
    {

        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(string fullname, string username, List<string> Roles)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

            var claims = new List<Claim>()
            {
                new Claim("UserName",username),
                new Claim("FullName",fullname),

            };

            claims.AddRange(Roles.Select(x => new Claim(
                ClaimTypes.Role, x

                )));

            var token = new JwtSecurityToken(expires: DateTime.Now.AddMinutes(10),
                 signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256), claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
