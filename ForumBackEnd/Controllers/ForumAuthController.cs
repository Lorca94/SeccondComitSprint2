using ForumBackEnd.Controllers.DTO;
using ForumBackEnd.Models;
using ForumBackEnd.Services;
using ForumBackEnd.Services.PasswordRepository;
using ForumBackEnd.Services.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForumBackEnd.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ForumAuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtServices _jwtServices;
        private readonly IPasswordRepository _enconder;

        public ForumAuthController(IUserRepository userRepository, JwtServices jwtServices, IPasswordRepository enconder)
        {
            _userRepository = userRepository;
            _jwtServices = jwtServices;
            _enconder = enconder;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            // Valor inicial de response
            IActionResult response = BadRequest();
            if (ModelState.IsValid)
            {
                    if (!_userRepository.ExistsByEmail(user.Email))
                    {
                        // Hash a la password
                        string hashed = _enconder.HashPassword(user.Password);
                        // Se crea el usuario
                        User newUser = new User()
                        {
                            Email = user.Email,
                            Username = user.Username,
                            Password = hashed,
                            RoleName = user.RoleName,
                        };
                        _userRepository.Create(user);
                        response = Ok(newUser);
                    }
            }
            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login (LoginDTO dto)
        {
            IActionResult response = Unauthorized();
            User user = _userRepository.FindByEmail(dto.Email);
            if(user != null)
            {
                if(_enconder.VerifyPassword(user.Password, dto.Password))
                {
                    string newToken = _jwtServices.GenerateJWT(user);
                    if(newToken != null)
                    {
                        response = Ok(new { token = newToken});
                    }
                }
            }
            return response;
        }
    }
}
