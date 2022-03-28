using ForumBackEnd.Controllers.DTO;
using ForumBackEnd.Models;
using ForumBackEnd.Services;
using ForumBackEnd.Services.PasswordRepository;
using ForumBackEnd.Services.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ForumBackEnd.Controllers.FinalControllers.Auth
{
    [AllowAnonymous]
    [Route("foro/auth")]
    [ApiController]
    public class AuthUserController: ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtServices _jwtServices;
        private readonly IPasswordRepository _enconder;

        public AuthUserController(IUserRepository userRepository, IPasswordRepository enconder, JwtServices jwtServices)
        {
            _userRepository = userRepository;
            _enconder = enconder;
            _jwtServices = jwtServices;
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDTO register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            User existingUserByEmail = _userRepository.FindByEmail(register.Email);
            if (existingUserByEmail != null)
            {
                return Conflict();
            }
            if (register.Password != register.ConfirmPassword)
            {
                return BadRequest();
            }
            string passwordHash = _enconder.HashPassword(register.Password);
            User user = new User()
            {
                Email = register.Email,
                Username = register.UserName,
                Password = passwordHash,
                RoleName = register.RoleName,
            };
            _userRepository.Create(user);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginDTO login)
        {
            IActionResult response = Unauthorized();

            User user = _userRepository.FindByEmail(login.Email);

            if (user != null)
            {
                if (_enconder.VerifyPassword(user.Password, login.Password))
                {
                    string tokenStr = _jwtServices.GenerateJWT(user);
                    if (tokenStr != null)
                    {
                        response = Ok(new { token = tokenStr });
                    }
                }
            }
            return response;
        }
    }
}
