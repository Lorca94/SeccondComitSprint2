using ForumBackEnd.Controllers.DTO;
using ForumBackEnd.DTO;
using ForumBackEnd.Models;
using ForumBackEnd.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ForumBackEnd.Controllers
{
    [Route("forum/auth")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserServices userServices;
        private readonly IConfiguration configuration;

        public AccountController(UserServices userServices,
            IConfiguration configuration)
        {
            this.userServices = userServices;
            this.configuration = configuration;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                User user = new User() { Email = userDTO.Email, Username = userDTO.UserName, Password = userDTO.Password, RoleId = userDTO.RoleId };
                // Si el usuario es 
                if (userServices.ValidateUser(user))
                {
                    int proccess = userServices.CreateUser(user);
                    switch (proccess)
                    {
                        case -1:
                            return BadRequest(new MessageDTO { Message = "Necesitas un rol válido para crear un usuario" });
                        case 0:
                            return BadRequest(new MessageDTO { Message = "Email/Username en uso" });
                        case 1:
                            return Ok(new MessageDTO { Message = "Usuario creado con éxito" });
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO userInfo)
        {
            if (ModelState.IsValid)
            {
                LoginDTO login = new LoginDTO() { Email = userInfo.Email, Password = userInfo.Password };
                var result = userServices.UserLogin(login);
                if(result)
                {
                    return BuildToken(userInfo);
                } else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attemp");
                    return BadRequest(ModelState);
                }
            } 
            else
            {
                return BadRequest(ModelState);
            }
        }

        private IActionResult BuildToken (UserDTO userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim("myValue","same value tham myValue"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Super_Secret_Ob_Key:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(3);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                expires: expiration,
                signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expiration
            });
        }
    }
}
