using Booking.Infrastructure.DataSource.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Booking.Infrastructure.Auth
{
    public interface ITokenGenerator
    {       
        string GenerateToken(ApplicationUser applicationUser, List<string> roles);
    }
    
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public string GenerateToken(ApplicationUser applicationUser, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, applicationUser.Email ?? ""),
                new Claim(ClaimTypes.Name, applicationUser.NormalizedUserName ?? ""),
                new Claim(ClaimTypes.NameIdentifier, applicationUser.Id),
            };

            roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList().ForEach(claims.Add);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"] ?? ""));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenAsString;
        }
    }
}
