using ForumBackEnd.Controllers.DTO;
using ForumBackEnd.Models;
using ForumBackEnd.Services.PasswordRepository;
using ForumBackEnd.Services.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ForumBackEnd.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordRepository _enconder;
        private readonly IConfiguration _configuration;

        public AuthController(IUserRepository userRepository, IPasswordRepository passwordRepository)
        {
            _userRepository = userRepository;
            _enconder = passwordRepository;
        }

        [HttpPost("Hi")]
        public string Hi()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userName = claim[0].Value;
            var email = claim[1].Value;
            var role = claim[3].Value;
            return "Wellcome to: " + role;
        }


        [Authorize(Roles="Administrador")]
        [HttpGet("GetValue")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Value1", "Value2", "Value3" };
        }
    }
}
