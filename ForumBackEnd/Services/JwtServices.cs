using ForumBackEnd.Data;
using ForumBackEnd.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ForumBackEnd.Services
{
    public class JwtServices
    {
        private readonly IConfiguration _configuration;
        private readonly ForumBackEndContext _context;

        public JwtServices(IConfiguration configuration, ForumBackEndContext context)
        {
            
            _configuration = configuration;
            _context = context;
        }

        public string GenerateJWT(User userInfo)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Super_Secret_Ob_Key:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(3);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName,userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Email,userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,userInfo.RoleName)
            };

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims,
                expires: expiration,
                signingCredentials: credentials
                );
            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodeToken;
        }
    }
}
